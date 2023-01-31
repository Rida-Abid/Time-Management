using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
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
        public IActionResult Add(TeacherRecord teacherRecord)
        {
            AddTeacher(teacherRecord);
            return View();
        }
        private void AddTeacher(TeacherRecord teacherRecord)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Teacher(Title, Firstname, Surname, Email) VALUES('{teacherRecord.Title}', '{teacherRecord.Firstname}', '{teacherRecord.Surname}', '{teacherRecord.Email}')";
                dbConnection.Open();
                dbConnection.Execute(sql, teacherRecord);
            }
            

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

    

        public class TeacherRecord
        {
            public int TeacherID;
            public string Title;
            public string Firstname;
            public string Surname;
            public string Email;
            public DateTime DateCreated;

        }




    }
}
