INSERT INTO Users (Id, Username, PasswordHash, Role, CreatedAt) VALUES
(NEWID(), 'teacher1', 'hashed_password_teacher1', 'Teacher', GETDATE()),
(NEWID(), 'teacher2', 'hashed_password_teacher2', 'Teacher', GETDATE()),
(NEWID(), 'student1', 'hashed_password_student1', 'Student', GETDATE()),
(NEWID(), 'student2', 'hashed_password_student2', 'Student', GETDATE()),
(NEWID(), 'student3', 'hashed_password_student3', 'Student', GETDATE());

INSERT INTO Exams (Id, Title, Description, StartTime, Duration, AuthorId) VALUES
(NEWID(), 'Math Exam', 'Exam for algebra and geometry.', '2025-01-10 10:00:00', 90, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher')),
(NEWID(), 'Physics Exam', 'Basics of mechanics and thermodynamics.', '2025-01-11 12:00:00', 120, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher')),
(NEWID(), 'History Exam', 'Medieval and modern history.', '2025-01-12 09:00:00', 60, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher')),
(NEWID(), 'English Exam', 'Grammar and vocabulary.', '2025-01-13 14:00:00', 45, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher')),
(NEWID(), 'Biology Exam', 'Plant and human biology.', '2025-01-14 16:00:00', 75, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'));

INSERT INTO Questions (Id, ExamId, QuestionText, CorrectAnswer, Points) VALUES
(NEWID(), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Math Exam'), 'What is 2 + 2?', '4', 3),
(NEWID(), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Math Exam'), 'Solve x in 2x = 10.', '5', 2),
(NEWID(), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Physics Exam'), 'What is the formula for force?', 'F = ma', 5),
(NEWID(), (SELECT TOP 1 Id FROM Exams WHERE Title = 'History Exam'), 'Who discovered America?', 'Christopher Columbus', 2),
(NEWID(), (SELECT TOP 1 Id FROM Exams WHERE Title = 'English Exam'), 'What is the synonym of "quick"?', 'fast', 1);

INSERT INTO Results (Id, UserId, ExamId, Score, CompletedAt) VALUES
(NEWID(), (SELECT TOP 1 Id FROM Users WHERE Username = 'student1'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Math Exam'), 85, GETDATE()),
(NEWID(), (SELECT TOP 1 Id FROM Users WHERE Username = 'student2'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Physics Exam'), 90, GETDATE()),
(NEWID(), (SELECT TOP 1 Id FROM Users WHERE Username = 'student3'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'History Exam'), 75, GETDATE()),
(NEWID(), (SELECT TOP 1 Id FROM Users WHERE Username = 'student1'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'English Exam'), 88, GETDATE()),
(NEWID(), (SELECT TOP 1 Id FROM Users WHERE Username = 'student2'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Biology Exam'), 95, GETDATE());

INSERT INTO Notifications (Id, Message, CreatedAt) VALUES
(NEWID(), 'The Math Exam is scheduled for 2025-01-10.', GETDATE()),
(NEWID(), 'Physics Exam results are out.', GETDATE()),
(NEWID(), 'History Exam will start in 1 hour.', GETDATE()),
(NEWID(), 'New English Exam is added.', GETDATE()),
(NEWID(), 'Biology Exam has been rescheduled.', GETDATE());