

Create table dbo.Subject
(
	SubjectID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name varchar(15) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO dbo.Subject (Name)
VALUES 
( 'English'),
( 'Urdu'),
( 'Mathematics'),
( 'Science'),
( 'Social Studies'),
( 'Islamiat'),
( 'Nazra'),
( 'Tarjuma tul Quran'),
( 'Computer'),
( 'Art'),
( 'Physics'),
( 'Biology'),
( 'Chemistry'),
( 'Pak Studies')



 