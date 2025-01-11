using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Data.Models;

public class Exam
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartTime { get; set; }

    public int Duration { get; set; }

    [Required] public Guid AuthorId { get; set; }
    public virtual User? Author { get; set; } 

    [Required] public Guid StatusId { get; set; }
    public Status? Status { get; set; } 

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}