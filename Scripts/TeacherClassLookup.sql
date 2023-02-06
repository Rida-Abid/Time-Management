
use [tms]
CREATE TABLE dbo.TeacherClassLookup
(   
     TeacherID int NOT NULL FOREIGN KEY REFERENCES dbo.Teacher(TeacherID), 
     ClassID int NOT NULL FOREIGN KEY REFERENCES dbo.Class(ClassID)
)
