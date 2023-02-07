using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class LessonController : Controller
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

            return View(GetLessons());
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(GetLessonById(Id));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            DeleteLesson(id);
            return RedirectToAction("Index");
        }
        private void DeleteLesson(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Lesson WHERE LessonID = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(LessonRecord lessonRecord, string startTime, string endTime)
        {
            AddLesson(lessonRecord, startTime, endTime);
            return View();
        }
        private void AddLesson(LessonRecord lessonRecord, string startTime, string endTime)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"INSERT INTO Lesson(StartTime, EndTime) VALUES('{startTime}','{endTime}')";
                dbConnection.Open();
                dbConnection.Execute(sql, lessonRecord);
            }


        }



        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string startTime, string endTime)
        {
            UpdateLessonById(Id, startTime, endTime);
            return View();
        }


        public IEnumerable<LessonRecord> GetLessons()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Lesson";
                dbConnection.Open();
                return dbConnection.Query<LessonRecord>(sql);
            }
        }

        public LessonRecord GetLessonById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Lesson  WHERE LessonID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<LessonRecord>(sql).FirstOrDefault();
            }
        }


        public void UpdateLessonById(int Id, string startTime, string endTime)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Lesson SET StartTime='{startTime}', EndTime='{endTime}'  WHERE LessonID = {Id}";
                dbConnection.Open();
                var result = dbConnection.Execute(sql) == 1;
            }
        }

    }
    public class LessonRecord
    {
        public int LessonID;
        public int LessonNo;
        public DateTime StartTime;
        public DateTime EndTime;

    }
}