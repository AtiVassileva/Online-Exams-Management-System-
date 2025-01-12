--- 1. Run to create the db 
CREATE DATABASE OnlineExamSystem;

--- 2. Run to switch to the db 
USE OnlineExamSystem;

--- 3. Run to create the tables 
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- Student, Teacher
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Statuses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(50) NOT NULL UNIQUE,
);

CREATE TABLE Exams (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    StartTime DATETIME NOT NULL,
    Duration INT NOT NULL, -- In minutes
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    StatusId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Exams_Author FOREIGN KEY (AuthorId) REFERENCES Users(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Exams_Status FOREIGN KEY (StatusId) REFERENCES Statuses(Id) ON DELETE NO ACTION
);


CREATE TABLE Questions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ExamId UNIQUEIDENTIFIER NOT NULL, 
    QuestionText NVARCHAR(500) NOT NULL,
    CorrectAnswer NVARCHAR(100),
	Points INT NOT NULL,
    CONSTRAINT FK_Questions_Exam FOREIGN KEY (ExamId) REFERENCES Exams(Id) ON DELETE NO ACTION
);

CREATE TABLE Results (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL, 
    ExamId UNIQUEIDENTIFIER NOT NULL, 
    Score INT NOT NULL,
    CompletedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Results_User FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Results_Exam FOREIGN KEY (ExamId) REFERENCES Exams(Id) ON DELETE NO ACTION
);

CREATE TABLE StudentsExams (
    StudentId UNIQUEIDENTIFIER NOT NULL,
    ExamId UNIQUEIDENTIFIER NOT NULL,
	PRIMARY KEY (StudentId, ExamId)
);