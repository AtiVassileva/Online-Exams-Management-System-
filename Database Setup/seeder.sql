INSERT INTO Users (Id, Username, PasswordHash, Role, CreatedAt) VALUES
(NEWID(), 'teacher1', '3e3588b9f023ec5eb6d53d421c8674c1c1ee1c16f027718b337165ec9ffe9934', 'Teacher', GETDATE()), -- raw password: hashed_password_teacher1
(NEWID(), 'student1', 'd52e919a3b93684cb66acea73a9280642b6e4e3d33248ff9d5f582faedb1596d', 'Student', GETDATE()), -- raw password: hashed_password_student1
(NEWID(), 'student2', 'b6bb1b87260483da1c25f0226e5759549937fcd69e7a0ddb64cd710efba1ca43', 'Student', GETDATE()), -- raw password: hashed_password_student2
(NEWID(), 'student3', '4319c38835a098624dbab7948cbc51ea3065677b869d52a200e13a0ee712b239', 'Student', GETDATE()); -- raw password: hashed_password_student3

INSERT INTO Statuses (Id, Name) VALUES
(NEWID(), 'Upcoming'),
(NEWID(), 'Ongoing'),
(NEWID(), 'Completed'),
(NEWID(), 'Cancelled');

INSERT INTO Exams (Id, Title, Description, StartTime, Duration, AuthorId, StatusId) VALUES
(NEWID(), 'Math Exam', 'Exam for algebra and geometry.', '2025-01-10 10:00:00', 90, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'), 
(SELECT TOP 1 Id FROM Statuses WHERE Name = 'Upcoming')),
(NEWID(), 'Physics Exam', 'Basics of mechanics and thermodynamics.', '2025-01-11 12:00:00', 120, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'), (SELECT TOP 1 Id FROM Statuses WHERE Name = 'Upcoming')),
(NEWID(), 'History Exam', 'Medieval and modern history.', '2025-01-12 09:00:00', 60, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'), (SELECT TOP 1 Id FROM Statuses WHERE Name = 'Ongoing')),
(NEWID(), 'English Exam', 'Grammar and vocabulary.', '2025-01-13 14:00:00', 45, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'), (SELECT TOP 1 Id FROM Statuses WHERE Name = 'Completed')),
(NEWID(), 'Biology Exam', 'Plant and human biology.', '2025-01-14 16:00:00', 75, (SELECT TOP 1 Id FROM Users WHERE Role = 'Teacher'), (SELECT TOP 1 Id FROM Statuses WHERE Name = 'Cancelled'));

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

INSERT INTO StudentsExams(StudentId, ExamId) VALUES
((SELECT TOP 1 Id FROM Users Where Username = 'student1'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Physics Exam')),
((SELECT TOP 1 Id FROM Users Where Username = 'student2'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Biology Exam')),
((SELECT TOP 1 Id FROM Users Where Username = 'student3'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'English Exam')),
((SELECT TOP 1 Id FROM Users Where Username = 'student1'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'History Exam')),
((SELECT TOP 1 Id FROM Users Where Username = 'student2'), (SELECT TOP 1 Id FROM Exams WHERE Title = 'Math Exam'));