
use [tms]
CREATE TABLE dbo.TeacherSubjectLookup
(   
     TeacherID int NOT NULL, 
     SubjectID int NOT NULL
);

ALTER TABLE dbo.TeacherSubjectLookup WITH CHECK ADD CONSTRAINT FK_TeacherSubjectLookup_Teacher Foreign Key(TeacherID)
References dbo.Teacher (TeacherID)

ALTER TABLE dbo.TeacherSubjectLookup WITH CHECK ADD CONSTRAINT FK_TeacherSubjectLookup_Subject Foreign Key(SubjectID)
References dbo.Subject (SubjectID)
