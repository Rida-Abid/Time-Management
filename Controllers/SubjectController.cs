using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using TTMS.Models;
using System.Configuration;


namespace TTMS.Controllers
{
    //public class SubjectController : Controller
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly string _connectionString;

    //    public SubjectController(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //        _connectionString = _configuration.GetConnectionString("SqlConnection");
    //    }

    //    [Authorize]
    //    public async Task<IActionResult> Index()
    //    {
    //        var subjects = await GetSubjectsAsync();
    //        return View(subjects);
    //    }

    //    [Authorize]
    //    public IActionResult AddEdit()
    //    {
    //        return View();
    //    }

    //    private async Task<List<SubjectViewModel>> GetSubjectsAsync()
    //    {
    //        var query = "SELECT SubjectID, SubjectName FROM Subject";
    //        using var connection = new SqlConnection(_connectionString);
    //        var subjects = await connection.QueryAsync<SubjectViewModel>(query);
    //        return subjects.ToList();
    //    }

    //}

    public class SubjectController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {

            return View(GetSubjects());
        }

        public IActionResult Delete(int id)
        {
        SubjectController subject = new SubjectController();
        subject.DeleteSubject(id);
        return RedirectToAction("Index");

        }

        public void DeleteSubject(int id)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM SUBJECT WHERE SubjectID = @Id";
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
        public IActionResult AddEdit(SubjectRecord subjecttable)
        {
            SubjectController sub = new SubjectController();
            sub.AddSubject( subjecttable);
            return RedirectToAction("Index");
        }

        public void AddSubject(SubjectRecord subjecttable)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Subject(Name) Values(@Name)";
                sql += " SELECT SCOPE_IDENTITY()";
                SqlDataAdapter ad = new SqlDataAdapter();
                DataSet ds = new DataSet();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection = connection;
                    

                    //command.ExecuteNonQuery();
                    
                    //command.Parameters.AddWithValue("@Id", subjecttable.SubjectID) ;
                    command.Parameters.AddWithValue("@Name", subjecttable.Name);
                    connection.Open();
                    subjecttable.SubjectID = Convert.ToInt32 (command.ExecuteScalar());
                    ad.Fill(ds,"Subject");
                    connection.Close();
                    
                }
            }

        }
        public List<SubjectRecord> GetSubjects()
        {

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";
            var listSubjects = new List<SubjectRecord>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Subject";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SubjectRecord subjecttable = new SubjectRecord();
                            subjecttable.SubjectID =  reader.GetInt32(0);
                            subjecttable.Name = reader.GetString(1);
                            subjecttable.DateCreated = reader.GetDateTime(2);

                            listSubjects.Add(subjecttable);
                        }
                    }
                }
            }
            return listSubjects;
        }
    }



    public class SubjectRecord
    {
        public int SubjectID;
        public string Name;
        public DateTime DateCreated;

    }
}