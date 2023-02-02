using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static TTMS.Controllers.SubjectController;
using static TTMS.Controllers.TeacherController;

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
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetClassById(Id));
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
        public IActionResult Add(ClassRecord classRecord, string Name)
        {
            AddClass(classRecord, Name);
            return View();
        }
        private void AddClass(ClassRecord classRecord, string Name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Class(Name) VALUES('{Name}')";
                dbConnection.Open();
                dbConnection.Execute(sql, classRecord);
            }


        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            UpdateClassById(Id, Name);
            return View();
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

        public ClassRecord GetClassById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Class   WHERE ClassID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<ClassRecord>(sql).FirstOrDefault();
            }
        }


        public bool UpdateClassById(int Id, string Name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Class SET Name='{Name}'  WHERE ClassID = {Id}";
                dbConnection.Open();
                return dbConnection.Execute(sql) == 1;
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

