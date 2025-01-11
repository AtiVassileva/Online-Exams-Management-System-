using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Data.Models;

public class Question
{
    public Guid Id { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? CorrectAnswer { get; set; }

    public int Points { get; set; }

    [Required] public Guid ExamId { get; set; }
    public virtual Exam? Exam { get; set; }
}