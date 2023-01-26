using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TTMS.Controllers
{
    //public class ClassController : Controller
    //{
    //    [Authorize]
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    [Authorize]
    //    public IActionResult AddEdit()
    //    {
    //        return View();
    //    }


    //}
    public class ClassController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {

            return View(GetClasses());
        }

        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
        }
        public List<ClassRecord> GetClasses()
        {

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            var listClasses = new List<ClassRecord>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Class";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClassRecord classtable = new ClassRecord();
                            classtable.ClassID = "" + reader.GetInt32(0);
                            classtable.Name = reader.GetString(1);
                            classtable.DateCreated = reader.GetDateTime(2);

                            listClasses.Add(classtable);
                        }
                    }
                }
            }
            return listClasses;
        }
    }



    public class ClassRecord
    {
        public string ClassID;
        public string Name;
        public DateTime DateCreated;

    }
}

