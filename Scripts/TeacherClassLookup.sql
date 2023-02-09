
use [tms]
CREATE TABLE dbo.TeacherClassLookup
(   
     TeacherID int NOT NULL, 
     ClassID int NOT NULL
);

ALTER TABLE dbo.TeacherClassLookup WITH CHECK ADD CONSTRAINT FK_TeacherClassLookup_Teacher Foreign Key(TeacherID)
References dbo.Teacher (TeacherID)

ALTER TABLE dbo.TeacherClassLookup WITH CHECK ADD CONSTRAINT FK_TeacherClassLookup_Class Foreign Key(ClassID)
References dbo.Class (ClassID)
