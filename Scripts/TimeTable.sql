use [tms]
CREATE TABLE dbo.TimeTable
(   
     TeacherID int NOT NULL FOREIGN KEY REFERENCES dbo.Teacher(TeacherID),
     SubjectID int NOT NULL FOREIGN KEY REFERENCES dbo.Subject(SubjectID),
     ClassID int NOT NULL FOREIGN KEY REFERENCES dbo.Class(ClassID),
     LessonID int NOT NULL FOREIGN KEY REFERENCES dbo.Lesson(LessonID)
)