using System.Collections.Generic;
using QuizMaker.Core.AbstractEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMaker.Entities.Concrete
{
    public enum QuestionType
    {
        Classical,
        Test,
        TrueFalse,
    }

    public class Question:IEntity
    {
        [Key]
        public int Id { get; set; }

        public string MyQuestion { get; set; }

        public decimal Points { get; set; }

        public QuestionType Type { get; set; }

        public int Difficulty { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        public int LessonId { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<QuestionExamRelation> ContainsExams { get; set; }


    }
}