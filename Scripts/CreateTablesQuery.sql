CREATE TABLE dbo.Teacher 
(  
    TeacherID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	Email varchar (50) NOT NULL,

);
CREATE TABLE dbo.Level
(     
    LevelID int NOT NULL PRIMARY KEY IDENTITY(1,1),
    Name varchar (50) NOT NULL
	
);
CREATE TABLE dbo.Subject
(  
     SubjectID int NOT NULL PRIMARY KEY IDENTITY(1,1),
     Name varchar(50) NOT NULL
);
CREATE TABLE dbo.Rooms
(  
     RoomID int NOT NULL PRIMARY KEY IDENTITY(1,1),
     Name varchar(50) NOT NULL
);
CREATE TABLE dbo.Lesson
(  
     TeacherID int NOT NULL PRIMARY KEY IDENTITY(1,1),
     SubjectID Int NOT NULL PRIMARY KEY IDENTITY(1,1),
	 RoomID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	 Date varchar (50)
);
CREATE TABLE dbo.TeacherToSubject
(   
     TeacherID int NOT NULL, 
     SubjectID int NOT NULL 
)

