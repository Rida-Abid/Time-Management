using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static TTMS.Controllers.SubjectController;

namespace TTMS.Controllers
{
    public class ClassController : Controller
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

            return View(GetClasses());
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            DeleteClass(id);
            return RedirectToAction("Index");
        }
        private void DeleteClass(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Class WHERE ClassID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ClassRecord classRecord)
        {
            AddClass(classRecord);
            return View();
        }
        private void AddClass(ClassRecord classRecord)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Class(Name) VALUES('{classRecord.Name}')";
                dbConnection.Open();
                dbConnection.Execute(sql, classRecord);
            }


        }

        public IEnumerable<ClassRecord> GetClasses()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Class";
                dbConnection.Open();
                return dbConnection.Query<ClassRecord>(sql);
            }
        }
    }
    public class ClassRecord
    {
        public int ClassID;
        public string Name;
        public DateTime DateCreated;

    }
}

