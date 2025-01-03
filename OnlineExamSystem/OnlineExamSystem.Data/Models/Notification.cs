namespace OnlineExamSystem.Data.Models;

public class Notification
{
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}