

Create table dbo.Teacher
(
	TeacherID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	Title varchar(5) NOT NULL,
	Firstname varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	Subject varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO dbo.Teacher (Title, Firstname, Surname, Subject, Email)
VALUES 
( 'Ms','Sidra','Maqsood','English','Sidra123@gmail.com'),
( 'Ms','Iqra','Aftab','Urdu','IqraAftab@gmail.com'),
( 'Ms','Laiba','Saqib','Mathematics','Laibasaqib@gmail.com'),
( 'Mr','Ahmad','Jamal','Science','Ahmad256@gmail.com'),
( 'Ms','Sadia','Nawaz','Social Studies','Sadia@gmail.com')
 