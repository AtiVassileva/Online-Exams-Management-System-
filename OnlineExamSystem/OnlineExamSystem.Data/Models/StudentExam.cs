namespace OnlineExamSystem.Data.Models
{
    public class StudentExam
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; set; } = null!;

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; } 

        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public decimal? Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}