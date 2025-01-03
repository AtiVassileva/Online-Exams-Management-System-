namespace OnlineExamSystem.Data.Models;

public class Result
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ExamId { get; set; }

    public int Score { get; set; }

    public DateTime CompletedAt { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}