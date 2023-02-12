Create table dbo.Lessons
(
	LessonID int  NOT NULL PRIMARY KEY IDENTITY(1,1),
	LessonNo varchar(15) NOT NULL,
	Duration decimal NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
	
);

INSERT INTO dbo.Lessons (LessonNo, Duration)
VALUES 
( 'Lesson 1', '45.00'),
( 'Lesson 2', '40.00'),
( 'Lesson 3', '40.00'),
( 'Lesson 4', '40.00'),
( 'Lesson 5', '20.00'),
( 'Lesson 6', '40.00'),
( 'Lesson 7', '40.00'),
( 'Lesson 8', '40.00'),
( 'Lesson 9', '40.00')