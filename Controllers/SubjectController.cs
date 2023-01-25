using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TTMS.Models;

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

        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
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
                            subjecttable.SubjectID = "" + reader.GetInt32(0);
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
        public string SubjectID;
        public string Name;
        public DateTime DateCreated;

    }
}