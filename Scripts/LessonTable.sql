Create table dbo.Lesson
(
	LessonID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	StartTime Time NOT NULL,
	EndTime Time NOT NULL
	
);

INSERT INTO dbo.Lesson (StartTime, EndTime)
VALUES 
( '8:00:00', '8:40:00'),
( '8:40:00', '9:20:00'),
( '9:20:00', '10:00:00'),
( '10:00:00', '10:40:00'),
( '10:40:00', '11:00:00'),
( '11:00:00', '11:40:00'),
( '11:40:00', '12:20:00'),
( '12:20:00', '1:00:00'),
( '1:00:00', '1:40:00')