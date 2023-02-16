Create table dbo.Days
(
	DayID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name varchar(15) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
	
);

INSERT INTO dbo.Days (Name)
VALUES 
( 'Monday'),
( 'Tuesday'),
( 'Wednesday'),
( 'Thursday'),
( 'Friday')