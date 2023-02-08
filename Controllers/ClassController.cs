﻿using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TTMS.Controllers
{
    public class ClassController : Controller
    {
        private string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        [Authorize]
        public IActionResult Index()
        {

            return View(GetClasses());
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetClassById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            DeleteClass(Id);
            return RedirectToAction("Index");
        }
        private void DeleteClass(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"DELETE FROM [dbo].[TeacherClassLookup]WHERE ClassID = ({Id})";
                dbConnection.Open();
                dbConnection.Execute(sql);
                sql = $"DELETE FROM Class WHERE ClassID = ({Id})";
                dbConnection.Execute(sql);
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            AddClass(Name);
            return View();
        }
        private void AddClass(string Name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO[dbo].[TeacherClassLookup]([TeacherID],[ClassID]) VALUES ({1}, {1})";
                dbConnection.Open();
                var result = dbConnection.Execute(sql);
                sql = $"INSERT INTO Class(Name) VALUES('{Name}')";
                result = dbConnection.Execute(sql);
            }


        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            UpdateClassById(Id, Name);
            return View();
        }
              

        public IEnumerable<ClassRecord> GetClasses()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Class";
                dbConnection.Open();
                return dbConnection.Query<ClassRecord>(sql);
            }
        }

        public ClassRecord GetClassById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Class   WHERE ClassID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<ClassRecord>(sql).FirstOrDefault();
            }
        }


        public void UpdateClassById(int Id, string Name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE [dbo].[TeacherClassLookup] SET [ClassID] = ({4})  WHERE TeacherID = ({Id} ";
                dbConnection.Open();
                var result = dbConnection.Execute(sql) == 1;
                sql = $"UPDATE Class SET Name='{Name}'  WHERE ClassID = {Id}";
                result = dbConnection.Execute(sql) == 1;
            }
        }

    }
    public class ClassRecord
    {
        public int ClassID;
        public string Name;
        public DateTime DateCreated;

    }
}

