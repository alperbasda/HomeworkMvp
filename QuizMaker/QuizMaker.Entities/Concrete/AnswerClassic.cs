using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMaker.Entities.Concrete
{
    public class AnswerClassic
    {
        [Key]
        public int Id { get; set; }

        public string MyAnswer { get; set; }

        public bool IsTrue { get; set; }

        public decimal Highest { get; set; }

        public decimal Lowest { get; set; }

        public decimal Avarage { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public int QuestionId { get; set; }

    }
}