using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Delete(int id)
        {
            DeleteTeacher(id);
            return RedirectToAction("Index");

        }
        private void DeleteTeacher(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Teacher WHERE TeacherID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(TeacherRecord teacherRecord, string Title, string Firstname, string Surname, string Subject, string Email)
        {
            AddTeacher(teacherRecord, Title, Firstname, Surname, Subject, Email);
            return View();
        }
        private void AddTeacher(TeacherRecord teacherRecord, string Title, string Firstname, string Surname, string Subject, string Email)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Teacher(Title, Firstname, Surname, Subject, Email) VALUES('{Title}','{Firstname}','{Surname}','{Subject}','{Email}')";
                dbConnection.Open();
                dbConnection.Execute(sql, teacherRecord);
            }


        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult Edit(int Id, string Title, string Firstname, string Surname, string Subject, string Email)
        //{
        //    UpdateTeacherById(Id, Title, Firstname, Surname, Subject, Email);
        //    return View();
        //}
        


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

        public TeacherRecord UpdateTeacherById(int Id, string Title, string Firstname, string Surname, string Subject, string Email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Teacher SET Title=('{Title}'), Firstname=('{Firstname}'), Surname=('{Surname}'), Subject=('{Subject}'), Email=('{Email}') WHERE TeacherID = ({Id})";
                dbConnection.Open();
                return dbConnection.Query<TeacherRecord>(sql, new { Id, Title, Firstname, Surname, Subject, Email }).FirstOrDefault();
            }
        }





        public class TeacherRecord
        {
            public int TeacherID;
            public int Title;
            public string Firstname;
            public string Surname;
            public int Subject;
            public string Email;
            public DateTime DateCreated;

        }




    }
}
