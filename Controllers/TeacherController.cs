using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
    {
        private string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
        [Authorize]
        public IActionResult Index()
        {

            return View(GetTeachers());
        }

        [Authorize]
        public IActionResult Delete(TeacherRecord teacherRecord)
        {
            DeleteTeacher(teacherRecord);
            return RedirectToAction("Index");

        }

        private int DeleteTeacher(TeacherRecord teacherRecord)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var deletedRows = connection.Execute(@"Delete from Teacher Where TeacherID = @Id", new { Id = teacherRecord.TeacherID });
                connection.Close();
                return deletedRows;
            }
            //string connectionString ="Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    string sql = "DELETE FROM TEACHER WHERE TeacherID = @Id";
            //    SqlCommand command = new SqlCommand(sql , connection);
            //    SqlParameter paramId = new SqlParameter();
            //    paramId.ParameterName = "@Id";
            //    paramId.Value = id;
            //    command.Parameters.Add(paramId);
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //}
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(TeacherRecord teacherRecord)
        {
            
            AddTeacher(teacherRecord);
            return View();
        }


        private int AddTeacher(TeacherRecord teacherRecord)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute("INSERT INTO Teacher(Firstname, Surname, Email) Values(@Firstname, @Surname, @Email)", new {Firstname = teacherRecord.Firstname, teacherRecord.Surname, teacherRecord.Email });
                connection.Close();
                return affectedRows;
            }
            //    string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        string sql = "INSERT INTO Teacher(Title, Firstname, Surname, Email) Values(@Title, @Firstname, @Surname, @Email)";

            //        SqlCommand command = new SqlCommand(sql, connection);
            //        command.Parameters.Add(new SqlParameter { ParameterName = "@Title", Value = teacherRecord.Title });
            //        command.Parameters.Add(new SqlParameter { ParameterName = "@Firstname", Value = teacherRecord.Firstname });
            //        command.Parameters.Add(new SqlParameter { ParameterName = "@Surname", Value = teacherRecord.Surname });
            //        command.Parameters.Add(new SqlParameter { ParameterName = "@Email", Value = teacherRecord.Email });


            //        command.ExecuteNonQuery();
            //        connection.Close();


            //    }

        }
        public List<TeacherRecord> GetTeachers()
        {
                List<TeacherRecord> teachers = new List<TeacherRecord>();
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    teachers = connection.Query<TeacherRecord>("Select * from Teacher").ToList();
                    connection.Close();
                }
                return teachers;
        }


        //string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
        //var listTeachers = new List<TeacherRecord>();
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();
        //    string sql = "SELECT * FROM Teacher";
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                TeacherRecord teachertable = new TeacherRecord();
        //                teachertable.TeacherID =  reader.GetInt32(0);
        //                teachertable.Title = reader.GetString(1);
        //                teachertable.Firstname = reader.GetString(2);
        //                teachertable.Surname = reader.GetString(3);
        //                teachertable.Email = reader.GetString(4);
        //                teachertable.DateCreated = reader.GetDateTime(5);

        //                listTeachers.Add(teachertable);
        //            }

        //        }
        //    }
        //}
        //return listTeachers;
    

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
