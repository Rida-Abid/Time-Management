

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

INSERT INTO dbo.Teacher (Title, Firstname, Surname, Email)
VALUES 
( 'Ms','Sidra','Maqsood','Sidra123@gmail.com'),
( 'Ms','Iqra','Aftab','IqraAftab@gmail.com'),
( 'Ms','Laiba','Saqib','Laibasaqib@gmail.com'),
( 'Mr','Ahmad','Jamal','Ahmad256@gmail.com'),
( 'Ms','Sadia','Nawaz','Sadia@gmail.com')
 