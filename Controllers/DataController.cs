using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMS.Models;


namespace TTMS.Controllers
{
    public class DataController : Controller
    {
        //1. Get 
        //2. GetById (string Tablename, int Id)
        //3. UpdateById (string Tablename, int Id, string field)

        private readonly string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=tms;Integrated Security=True";

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        #region Teacher

        public bool AddTeacher(string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {
            InsertTeacher(Title, Firstname, Surname, Subjects, Classes, Email);
            return true;
        }

        public bool DeleteTeacher(int Id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"DELETE FROM TeacherSubjectLookup WHERE TeacherID = ({Id});";
            sql += $"DELETE FROM TeacherClassLookup WHERE TeacherID = ({Id});";
            sql += $"DELETE FROM Teacher WHERE TeacherID = ({Id})";
            return dbConnection.Execute(sql) == 3;

        }

        public IEnumerable<TeacherRecord> GetTeachers()
        {
            using IDbConnection dbConnection = Connection;

            string sql = @"SELECT * FROM Teacher";
            dbConnection.Open();
            return dbConnection.Query<TeacherRecord>(sql);
        }

        public TeacherRecord GetTeacherById(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"SELECT * FROM Teacher   WHERE TeacherID = {Id}";
            dbConnection.Open();
            return dbConnection.Query<TeacherRecord>(sql).FirstOrDefault();
        }

        public bool UpdateTeacherById(int Id, string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {
            UpdateTeacher(Id, Title, Firstname, Surname, Subjects, Classes, Email);
            return true;
        }

        private bool InsertTeacher(string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Teacher (Title, Firstname, Surname, Email) VALUES ('{Title}','{Firstname}','{Surname}','{Email}');SELECT SCOPE_IDENTITY();";
            var savedTeacherId = dbConnection.ExecuteScalar(sql);
            var listSubjectIds = Subjects.ToList();
            foreach (int subjectId in listSubjectIds)
            {
                sql = $"INSERT INTO TeacherSubjectLookup (TeacherID, SubjectID) VALUES ({savedTeacherId}, {subjectId})";
                dbConnection.Execute(sql);
            }
            var listClassIds = Classes.ToList();
            foreach (int classId in listClassIds)
            {
                sql = $"INSERT INTO TeacherClassLookup (TeacherID, ClassID) VALUES ({savedTeacherId}, {classId});";
                dbConnection.Execute(sql);
            }
            return true;
        }

        private bool UpdateTeacher(int Id,string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = "";
            var listSubjectIds = Subjects.ToList();
            foreach (int subjectId in listSubjectIds)
            {
                sql = $"UPDATE TeacherSubjectLookup SET SubjectID = ({subjectId}) WHERE TeacherID = ({Id};SELECT SCOPE_IDENTITY(); ";
                dbConnection.Execute(sql);
            }
            var listClassIds = Classes.ToList();
            foreach (int classId in listClassIds)
            {
                sql = $"UPDATE TeacherClassLookup SET ClassID = ({classId}) WHERE TeacherID = ({Id}); SELECT SCOPE_IDENTITY();";
                dbConnection.Execute(sql);
            }
             sql = $"UPDATE Teacher SET Title='{Title}', Firstname='{Firstname}', Surname='{Surname}', Email='{Email}' WHERE TeacherID = ({Id})";

            return true;
        }

        #endregion

        #region Subject

        public bool AddSubject(string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Subject(Name) VALUES('{Name}')";
            return dbConnection.Execute(sql) == 1;

        }

        public bool DeleteSubject(int Id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"DELETE FROM TeacherSubjectLookup WHERE SubjectID = ({Id});";
            sql += $"DELETE FROM Subject WHERE SubjectID = ({Id})";
            return dbConnection.Execute(sql) == 2;

        }

        public IEnumerable<SubjectRecord> GetSubjects()
        {
            using IDbConnection dbConnection = Connection;
            string sql = @"SELECT * FROM Subject";
            dbConnection.Open();
            return dbConnection.Query<SubjectRecord>(sql);

        }

        public SubjectRecord GetSubjectById(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"SELECT * FROM Subject   WHERE SubjectID = {Id}";
            dbConnection.Open();
            return dbConnection.Query<SubjectRecord>(sql).FirstOrDefault();
        }


        public bool UpdateSubjectById(int Id, string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"UPDATE Subject SET Name='{Name}'  WHERE SubjectID = {Id}";
            return dbConnection.Execute(sql) == 1;
        }
        #endregion

        #region Class

        public bool AddClass(string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Class(Name) VALUES('{Name}')";
            return dbConnection.Execute(sql) == 2;
        }

        public bool DeleteClass(int Id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"DELETE FROM TeacherClassLookup WHERE ClassID = ({Id});";
            sql += $"DELETE FROM Class WHERE ClassID = ({Id})";
            return dbConnection.Execute(sql) == 2;

        }

        public IEnumerable<ClassRecord> GetClasses()
        {
            using IDbConnection dbConnection = Connection;
            string sql = @"SELECT * FROM Class";
            dbConnection.Open();
            return dbConnection.Query<ClassRecord>(sql);
        }

        public ClassRecord GetClassById(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"SELECT * FROM Class   WHERE ClassID = {Id}";
            dbConnection.Open();
            return dbConnection.Query<ClassRecord>(sql).FirstOrDefault();
        }


        public bool UpdateClassById(int Id, string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"UPDATE Class SET Name='{Name}'  WHERE ClassID = {Id}";
            return dbConnection.Execute(sql) == 2;
        }
        #endregion

        #region Lesson


        public void AddLesson(string LessonNo, decimal Duration)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"INSERT INTO Lessons (LessonNo, Duration) VALUES('{LessonNo}','{Duration}')";
            dbConnection.Open();
            dbConnection.Execute(sql);

        }

        public void DeleteLesson(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"DELETE FROM Lessons WHERE LessonID = {Id}";
            dbConnection.Open();
            dbConnection.Execute(sql);

        }

        public IEnumerable<LessonRecord> GetLessons()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Lessons";
                dbConnection.Open();
                return dbConnection.Query<LessonRecord>(sql);
            }
        }

        public LessonRecord GetLessonById(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"SELECT * FROM Lessons  WHERE LessonID = {Id}";
                dbConnection.Open();
                return dbConnection.Query<LessonRecord>(sql).FirstOrDefault();
            }
        }


        public bool UpdateLessonById(int Id, string LessonNo, decimal Duration)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Lessons SET LessonNo ='{LessonNo}', Duration='{Duration}'  WHERE LessonID = {Id}";
                dbConnection.Open();
                return dbConnection.Execute(sql) == 1;
            }
        }
        #endregion

        #region Days


        public IEnumerable<DaysRecord> GetDays()
        {
            using IDbConnection dbConnection = Connection;
            string sql = @"SELECT * FROM Days";
            dbConnection.Open();
            return dbConnection.Query<DaysRecord>(sql);
        }

        public DaysRecord GetDaysById(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"SELECT * FROM Days  WHERE DayID = {Id}";
            dbConnection.Open();
            return dbConnection.Query<DaysRecord>(sql).FirstOrDefault();
        }
        #endregion

        #region Timetable

        public bool AddTimetable(string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Timetable(Name) VALUES('{Name}')";
            return dbConnection.Execute(sql) == 2;
        }

        public bool DeleteTimetable(int Id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            //string sql = $"DELETE FROM TeacherClassLookup WHERE ClassID = ({Id});";
            string sql = $"DELETE FROM Timetable WHERE TimetableID = ({Id})";
            return dbConnection.Execute(sql) == 2;

        }

        public IEnumerable<TimetableRecord> GetTimetables()
        {
            using IDbConnection dbConnection = Connection;
            string sql = @"SELECT * FROM Timetable";
            dbConnection.Open();
            return dbConnection.Query<TimetableRecord>(sql);
        }

        public TimetableRecord GetTimetableById(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"SELECT * FROM Timetable   WHERE TimetableID = {Id}";
            dbConnection.Open();
            return dbConnection.Query<TimetableRecord>(sql).FirstOrDefault();
        }


        public bool UpdatetimetableById(int Id, string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"UPDATE Timetable SET Name='{Name}'  WHERE TimetableID = {Id}";
            return dbConnection.Execute(sql) == 2;
        }
        #endregion
    }
}
