CREATE TABLE dbo.Teacher 
(  
    TeacherID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL

);
CREATE TABLE dbo.Class
(     
    ClassID int NOT NULL PRIMARY KEY IDENTITY(1,1),
    ClassTime float NOT NULL
	
);
CREATE TABLE dbo.Subject
(  
     SubjectID int NOT NULL PRIMARY KEY IDENTITY(1,1)
    ,SubjectName varchar(50) NOT NULL
);
CREATE TABLE dbo.TeacherToSubject
(   
     TeacherID int NOT NULL 
    ,SubjectID int NOT NULL 
)
Go
ALTER TABLE dbo.TeacherToSubject WITH CHECK ADD CONSTRAINT FK_TeacherToSubject_Teacher Foreign Key(TeacherID)
References dbo.Teacher (TeacherID)
Go
ALTER TABLE dbo.TeacherToSubject WITH CHECK ADD CONSTRAINT FK_TeacherToSubject Foreign Key(SubjectID)
References dbo.Subject (SubjectID)

CREATE TABLE dbo.SubjectToClass
(
     SubjectID int NOT NULL 
	 ,ClassID int NOT NULL 
	 ,SubjectOrder int NOT NULL 
)
Go
ALTER TABLE dbo.SubjectToClass WITH CHECK ADD CONSTRAINT FK_SubjectToClass_Subject Foreign Key(SubjectID)
References dbo.Subject (SubjectID)
Go
ALTER TABLE dbo.SubjectToClass WITH CHECK ADD CONSTRAINT FK_SubjectToClass_Class Foreign Key(ClassID)
References dbo.Class (ClassID)
