using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMaker.Entities.Concrete
{
    public class QuestionExamRelation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        public int ExamId { get; set; }
    }
}