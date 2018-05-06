using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Entities.Concrete
{
    public class Lesson :IEntity
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name="Ders Adı")]
        public string LessonName { get; set; }
        [Display(Name="Soru Sayısı")]
        public virtual ICollection<Question> QuestionInLesson { get; set; }
        [Display(Name="Sınav Sayısı")]
        public virtual ICollection<Exam> ExamInLesson { get; set; }

    }
}