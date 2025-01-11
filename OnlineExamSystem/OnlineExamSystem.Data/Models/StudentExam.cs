using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Data.Models
{
    public class StudentExam
    {
        public Guid Id { get; set; }
        [Required] public Guid StudentId { get; set; }
        public User? Student { get; set; }

        [Required] public Guid ExamId { get; set; }
        public Exam? Exam { get; set; } 

        [Required] public Guid StatusId { get; set; }
        public Status? Status { get; set; } 

        public decimal? Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}