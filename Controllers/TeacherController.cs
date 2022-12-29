using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
        }
    }

    public class TeacherData
    { 
        public List<TeacherTable> listTeachers = new List<TeacherTable>(); 
        public void OnGet()
        { 
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM Teacher";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        { 
                            while (reader.Read())
                            {
                                TeacherTable teachertable = new TeacherTable();
                                teachertable.TeacherID = "" + reader.GetInt32(0);
                                teachertable.Title = reader.GetString(1);
                                teachertable.Firstname = reader.GetString(2);
                                teachertable.Surname = reader.GetString(3);
                                teachertable.Email = reader.GetString(4);
                                teachertable.DateCreated = reader.GetString(5);

                                listTeachers.Add(teachertable);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        
        
        }
    }

        public class TeacherTable
        {
            public String TeacherID;
            public String Title;
            public String Firstname;
            public String Surname;
            public String Email;
            public String DateCreated;

        }



}
