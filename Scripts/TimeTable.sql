use [tms]
CREATE TABLE dbo.Timetable
(   

     TimetableID int NOT NULL PRIMARY KEY IDENTITY(1,1), 
     Name varchar(50) NOT NULL,
     TeacherID int NOT NULL,
     SubjectID int NOT NULL,
     ClassID int NOT NULL,
     LessonID int NOT NULL,
     DayId int NOT NULL

);
    
ALTER TABLE dbo.Timetable WITH CHECK ADD CONSTRAINT FK_Timetable_Teacher Foreign Key(TeacherID)
References dbo.Teacher (TeacherID)

ALTER TABLE dbo.Timetable WITH CHECK ADD CONSTRAINT FK_Timetable_Subject Foreign Key(SubjectID)
References dbo.Subject (SubjectID)

ALTER TABLE dbo.Timetable WITH CHECK ADD CONSTRAINT FK_Timetable_Class Foreign Key(ClassID)
References dbo.Class (ClassID)

ALTER TABLE dbo.Timetable WITH CHECK ADD CONSTRAINT FK_Timetable_Lesson Foreign Key(LessonID)
References dbo.Lessons (LessonID)

ALTER TABLE dbo.Timetable WITH CHECK ADD CONSTRAINT FK_Timetable_Day Foreign Key(DayID)
References dbo.Days (DayID)

USE [tms]
GO

INSERT INTO [dbo].[Timetable] (TeacherID,[SubjectID],[ClassID],[LessonID],[DayId]) VALUES (34,7,1,3,1)
INSERT INTO [dbo].[Timetable] (TeacherID,[SubjectID],[ClassID],[LessonID],[DayId]) VALUES (34,8,2,4,1)
INSERT INTO [dbo].[Timetable] (TeacherID,[SubjectID],[ClassID],[LessonID],[DayId]) VALUES (34,9,3,5,1)         

