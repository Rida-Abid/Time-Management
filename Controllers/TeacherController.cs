using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {

            return View(GetTeachers());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            TeacherController teacher= new TeacherController();
            teacher.DeleteTeacher( id);
            return RedirectToAction("Index");

        }

        public void DeleteTeacher(int id)
        {
            string connectionString ="Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM TEACHER WHERE TeacherID = @Id";
                SqlCommand command = new SqlCommand(sql , connection);
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
        }
        public List<TeacherRecord> GetTeachers()
        {

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            var listTeachers = new List<TeacherRecord>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Teacher";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TeacherRecord teachertable = new TeacherRecord();
                            teachertable.TeacherID = "" + reader.GetInt32(0);
                            teachertable.Title = reader.GetString(1);
                            teachertable.Firstname = reader.GetString(2);
                            teachertable.Surname = reader.GetString(3);
                            teachertable.Email = reader.GetString(4);
                            teachertable.DateCreated = reader.GetDateTime(5);

                            listTeachers.Add(teachertable);
                        }

                    }
                }
            }
            return listTeachers;
        }

        public class TeacherRecord
        {
            public string TeacherID;
            public string Title;
            public string Firstname;
            public string Surname;
            public string Email;
            public DateTime DateCreated;

        }




    }
}
