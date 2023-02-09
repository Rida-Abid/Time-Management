using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

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
        public IActionResult Delete(int Id)
        {
            DeleteClass(Id);
            return RedirectToAction("Index");
        }
        private bool DeleteClass(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"DELETE FROM TeacherClassLookup WHERE ClassID = ({Id});";
                sql += $"DELETE FROM Class WHERE ClassID = ({Id})";
                return dbConnection.Execute(sql) == 2;
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            AddClass(Name);
            return View();
        }
        private bool AddClass(string Name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"INSERT INTO TeacherClassLookup (TeacherID, ClassID) VALUES ({1}, {1});";
                sql += $"INSERT INTO Class(Name) VALUES('{Name}')";
                return dbConnection.Execute(sql) == 2;
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
                dbConnection.Open();
                string sql = $"UPDATE TeacherClassLookup SET ClassID = ({4}) WHERE TeacherID = ({Id};";
                sql += $"UPDATE Class SET Name='{Name}'  WHERE ClassID = {Id}";
                return dbConnection.Execute(sql) == 2;
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

