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
        public bool AddTeacher(string Title, string Firstname, string Surname, string Subject, string Email)
        {
    
                      
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Teacher (Title, Firstname, Surname, Subject, Email) VALUES ('{Title}','{Firstname}','{Surname}','{Subject}','{Email}');";
            sql += $"INSERT INTO TeacherClassLookup (TeacherID, ClassID) VALUES ({1}, {1});";
            sql += $"INSERT INTO TeacherSubjectLookup (TeacherID, SubjectID) VALUES ({1}, {1})";
            return dbConnection.Execute(sql) == 3;

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

        public bool UpdateTeacherById(int Id, string Title, string Firstname, string Surname, string Subject, string Email)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"UPDATE TeacherSubjectLookup SET SubjectID = ({4}) WHERE TeacherID = ({Id};";
            sql += $"UPDATE TeacherClassLookup SET ClassID = ({4}) WHERE TeacherID = ({Id};";
            sql += $"UPDATE Teacher SET Title='{Title}', Firstname='{Firstname}', Surname='{Surname}', Subject='{Subject}', Email='{Email}' WHERE TeacherID = ({Id})";
            return dbConnection.Execute(sql) == 3;
        }

        #endregion

        #region Subject

        public bool AddSubject(int Id, string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO Subject(Name) VALUES('{Name}');";
            sql += $"INSERT INTO TeacherSubjectLookup (TeacherID, SubjectID) VALUES ({1}, {Id})";
            return dbConnection.Execute(sql) == 2;

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
            string sql = $"UPDATE [dbo].[TeacherSubjectLookup] SET [SubjectID] = ({4})  WHERE TeacherID = ({Id};";
            sql += $"UPDATE Subject SET Name='{Name}'  WHERE SubjectID = {Id}";
            return dbConnection.Execute(sql) == 2;
        }
        #endregion

        #region Class

        public bool AddClass(string Name)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            string sql = $"INSERT INTO TeacherClassLookup (TeacherID, ClassID) VALUES ({1}, {1});";
            sql += $"INSERT INTO Class(Name) VALUES('{Name}')";
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
            string sql = $"UPDATE TeacherClassLookup SET ClassID = ({4}) WHERE TeacherID = ({Id};";
            sql += $"UPDATE Class SET Name='{Name}'  WHERE ClassID = {Id}";
            return dbConnection.Execute(sql) == 2;
        }
        #endregion

        #region Lesson


        public void AddLesson(string LessonNo, decimal Duration)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"INSERT INTO Lesson(LessonNo, Duration) VALUES('{LessonNo}','{Duration}')";
            dbConnection.Open();
            dbConnection.Execute(sql);

        }

        public void DeleteLesson(int Id)
        {
            using IDbConnection dbConnection = Connection;
            string sql = $"DELETE FROM Lesson WHERE LessonID = {Id}";
            dbConnection.Open();
            dbConnection.Execute(sql);

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


        public bool UpdateLessonById(int Id, string LessonNo, decimal Duration)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = $"UPDATE Lesson SET LessonNo ='{LessonNo}', Duration='{Duration}'  WHERE LessonID = {Id}";
                dbConnection.Open();
                return dbConnection.Execute(sql) == 1;
            }
        }
        #endregion
    }
}
