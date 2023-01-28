using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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

        public IActionResult Delete(int id)
        {
            ClassController Class = new ClassController();
            Class.DeleteClass(id);
            return RedirectToAction("Index");

        }

        public void DeleteClass(int id)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM CLASS WHERE ClassID = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [Authorize]
        public IActionResult AddEdit(ClassRecord classtable)
        {
            ClassController Class = new ClassController();
            Class.AddClass(classtable);
            return RedirectToAction("Index");
        }

        public void AddClass(ClassRecord classtable)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Class(Name) Values(@Name)";
                sql += " SELECT SCOPE_IDENTITY()";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                {
                    command.Connection = connection;


                    //command.ExecuteNonQuery();

                    //command.Parameters.AddWithValue("@Id", subjecttable.SubjectID) ;
                    command.Parameters.AddWithValue("@Name", classtable.Name);
                    connection.Open();
                    classtable.ClassID = Convert.ToInt32(command.ExecuteScalar());
                    //ad.Fill(ds, "Class");
                    connection.Close();

                }
            }

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
                            classtable.ClassID =reader.GetInt32(0);
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
        public int? ClassID;
        public string? Name;
        public DateTime DateCreated;

    }
}

