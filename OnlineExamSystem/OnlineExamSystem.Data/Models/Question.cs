namespace OnlineExamSystem.Data.Models;

public class Question
{
    public Guid Id { get; set; }

    public Guid ExamId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? CorrectAnswer { get; set; }

    public int Points { get; set; }

    public virtual Exam Exam { get; set; } = null!;
}