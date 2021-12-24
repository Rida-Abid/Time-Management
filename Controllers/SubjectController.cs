using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SubjectController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var subjects = await GetSubjectsAsync();
            return View(subjects);
        }

        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
        }

        private async Task<List<SubjectViewModel>> GetSubjectsAsync()
        {
            var query = "SELECT SubjectID, SubjectName FROM Subject";
            using var connection = new SqlConnection(_connectionString);
            var subjects = await connection.QueryAsync<SubjectViewModel>(query);
            return subjects.ToList();
        }

    }
}