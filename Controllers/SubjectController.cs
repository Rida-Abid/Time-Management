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
    public class SubjectController : Controller
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

            return View(GetSubjects());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            DeleteSubject(id);
            return RedirectToAction("Index");

        }
        private void DeleteSubject(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Subject WHERE SubjectID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(SubjectRecord subjectRecord)
        {
            AddSubject(subjectRecord);
            return View();
        }
        private void AddSubject(SubjectRecord subjectRecord)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Subject(Name) VALUES('{subjectRecord.Name}')";
                dbConnection.Open();
                dbConnection.Execute(sql, subjectRecord);
            }


        }
        public IEnumerable<SubjectRecord> GetSubjects()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Subject";
                dbConnection.Open();
                return dbConnection.Query<SubjectRecord>(sql);
            }
        }


    }
    public class SubjectRecord
    {
        public int SubjectID;
        public string Name;
        public DateTime DateCreated;

    }
}