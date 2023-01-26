

Create table dbo.Class
(
	ClassID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name varchar(15) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UpdatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO dbo.Class (Name)
VALUES 
( 'Class 1'),
( 'Class 2'),
( 'Class 3'),
( 'Class 4'),
( 'Class 5')