using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Data.Models
{
    public class StudentExam
    {
        [Required] public Guid StudentId { get; set; }
        public User? Student { get; set; }

        [Required] public Guid ExamId { get; set; }
        public Exam? Exam { get; set; }
    }
}