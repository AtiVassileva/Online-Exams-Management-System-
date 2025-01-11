using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Data.Models;

public class Result
{
    public Guid Id { get; set; }

    [Required] public Guid UserId { get; set; }

    [Required]  public Guid ExamId { get; set; }

    public int Score { get; set; }

    public DateTime CompletedAt { get; set; }

    public virtual Exam? Exam { get; set; } 

    public virtual User? User { get; set; } 
}