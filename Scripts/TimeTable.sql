use [tms]
CREATE TABLE dbo.TimeTable
(   

     TimetableID int NOT NULL PRIMARY KEY IDENTITY(1,1), 
     Name varchar(15) NOT NULL,
     TeacherID int NOT NULL,
     SubjectID int NOT NULL,
     ClassID int NOT NULL,
     LessonID int NOT NULL

);
    
ALTER TABLE dbo.TimeTable WITH CHECK ADD CONSTRAINT FK_TimeTable_Teacher Foreign Key(TeacherID)
References dbo.Teacher (TeacherID)

ALTER TABLE dbo.TimeTable WITH CHECK ADD CONSTRAINT FK_TimeTable_Subject Foreign Key(SubjectID)
References dbo.Subject (SubjectID)

ALTER TABLE dbo.TimeTable WITH CHECK ADD CONSTRAINT FK_TimeTable_Class Foreign Key(ClassID)
References dbo.Class (ClassID)

ALTER TABLE dbo.TimeTable WITH CHECK ADD CONSTRAINT FK_TimeTable_Lesson Foreign Key(LessonID)
References dbo.Lessons (LessonID)