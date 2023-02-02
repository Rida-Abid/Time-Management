using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class SubjectController : Controller
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

            return View(GetSubjects());
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetSubjectById(Id));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            DeleteSubject(id);
            return RedirectToAction("Index");
        }
        private void DeleteSubject(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Subject WHERE SubjectID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(SubjectRecord subjectRecord, string Name)
        {
            AddClass(subjectRecord, Name);
            return View();
        }
        private void AddClass(SubjectRecord subjectRecord, string Name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Subject(Name) VALUES('{Name}')";
                dbConnection.Open();
                dbConnection.Execute(sql, subjectRecord);
            }


        }



        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            UpdateSubjectById(Id, Name);
            return View();
        }


        public IEnumerable<SubjectRecord> GetSubjects()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Subject";
                dbConnection.Open();
                return dbConnection.Query<SubjectRecord>(sql);
            }
        }

        public SubjectRecord GetSubjectById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Subject   WHERE SubjectID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<SubjectRecord>(sql).FirstOrDefault();
            }
        }


        public void UpdateSubjectById(int Id, string Name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Subject SET Name='{Name}'  WHERE SubjectID = {Id}";
                dbConnection.Open();
                var result = dbConnection.Execute(sql) == 1;
            }
        }

    }
    public class SubjectRecord
    {
        public int SubjectID;
        public string Name;
        public DateTime DateCreated;

    }
}