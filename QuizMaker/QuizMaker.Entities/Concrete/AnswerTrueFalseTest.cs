using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Entities.Concrete
{
    public class AnswerTrueFalseTest:IAnswer
    {
        [Key]
        public int Id { get; set; }

        public string MyAnswer { get; set; }

        public bool IsTrue { get; set; }

        public int AnswerCount { get; set; }

        public int TrueAnswerCount { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}