using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;

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
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetSubjectById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            DeleteSubject(Id);
            return RedirectToAction("Index");
        }
        private bool DeleteSubject(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"DELETE FROM TeacherSubjectLookup WHERE SubjectID = ({Id});";
                sql += $"DELETE FROM Subject WHERE SubjectID = ({Id})";
                return dbConnection.Execute(sql) == 2;
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            AddClass( Name);
            return View();
        }
        private bool AddClass(string Name)
        {

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"INSERT INTO Subject(Name) VALUES('{Name}');";
                sql += $"INSERT INTO TeacherSubjectLookup (TeacherID, SubjectID) VALUES ({1}, {1})";
                return dbConnection.Execute(sql) == 2;
                
            }


        }



        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            UpdateSubjectById(Id, Name);
            return View();
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

        public SubjectRecord GetSubjectById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Subject   WHERE SubjectID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<SubjectRecord>(sql).FirstOrDefault();
            }
        }


        public bool UpdateSubjectById(int Id, string Name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string sql = $"UPDATE [dbo].[TeacherSubjectLookup] SET [SubjectID] = ({4})  WHERE TeacherID = ({Id};";
                sql += $"UPDATE Subject SET Name='{Name}'  WHERE SubjectID = {Id}";
                return dbConnection.Execute(sql) == 2;
                
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