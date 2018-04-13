using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMaker.Entities.Concrete
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        public int LessonId { get; set; }

        public string Observer { get; set; }

        [InverseProperty("Exam")]
        public virtual ICollection<QuestionExamRelation> QuestionInExam { get; set; }

    }
}