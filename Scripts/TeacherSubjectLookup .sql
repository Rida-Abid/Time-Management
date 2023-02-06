
use [tms]
CREATE TABLE dbo.TeacherSubjectLookup
(   
     TeacherID int NOT NULL FOREIGN KEY REFERENCES dbo.Teacher(TeacherID), 
     SubjectID int NOT NULL FOREIGN KEY REFERENCES dbo.Subject(SubjectID)
)
