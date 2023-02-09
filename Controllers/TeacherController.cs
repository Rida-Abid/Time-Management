using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;
using static TTMS.Controllers.TeacherController;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
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
            return View(GetTeachers());
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetTeacherById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            DeleteTeacher(Id);
            return RedirectToAction("Index");

        }
        private bool DeleteTeacher(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"DELETE FROM TeacherSubjectLookup WHERE TeacherID = ({Id});";
                sql += $"DELETE FROM TeacherClassLookup WHERE TeacherID = ({Id});";
                sql += $"DELETE FROM Teacher WHERE TeacherID = ({Id})";
                return dbConnection.Execute(sql) == 3;
               
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Title, string Firstname, string Surname, string Subject, string Email)
        {
            AddTeacher(Title, Firstname, Surname, Subject, Email);
            return View();
        }
        private bool AddTeacher(string Title, string Firstname, string Surname, string Subject, string Email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"INSERT INTO Teacher (Title, Firstname, Surname, Subject, Email) VALUES ('{Title}','{Firstname}','{Surname}','{Subject}','{Email}');";
                sql += $"INSERT INTO TeacherClassLookup (TeacherID, ClassID) VALUES ({1}, {1});";
                sql +=  $"INSERT INTO TeacherSubjectLookup (TeacherID, SubjectID) VALUES ({1}, {1})";
                return dbConnection.Execute(sql) == 3;

            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Title, string Firstname, string Surname,  string Email)
        {
            UpdateTeacherById(Id, Title, Firstname, Surname,  Email);
            return View();
        }



        public IEnumerable<TeacherRecord> GetTeachers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                
                string sql = @"SELECT * FROM Teacher";
                dbConnection.Open();
                return dbConnection.Query<TeacherRecord>(sql);
            }
        }
   
        public TeacherRecord GetTeacherById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Teacher   WHERE TeacherID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<TeacherRecord>(sql).FirstOrDefault();
            }
        }

        public bool UpdateTeacherById(int Id, string Title, string Firstname, string Surname, string Email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"UPDATE TeacherSubjectLookup SET SubjectID = ({4}) WHERE TeacherID = ({Id};";
                sql += $"UPDATE TeacherClassLookup SET ClassID = ({4}) WHERE TeacherID = ({Id};";
                sql +=  $"UPDATE Teacher SET Title='{Title}', Firstname='{Firstname}', Surname='{Surname}', Email='{Email}' WHERE TeacherID = ({Id})";
                return dbConnection.Execute(sql) == 3;
            }
        }





        public class TeacherRecord
        {
            public int TeacherID;
            public string Title;
            public string Firstname;
            public string Surname;
            public string Subject;
            public string Email;
            public DateTime DateCreated;

        }




    }
}
