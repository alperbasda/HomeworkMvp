using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Entities.Concrete
{
    public class Exam : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Sınav Adı")]
        public string Name { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        [NotMapped]
        [Display(Name = "Ders Adı")]
        public string LessonName => Lesson.LessonName;

        public int? LessonId { get; set; }
        [Display(Name = "Gözetmen")]
        public string Observer { get; set; }

        [InverseProperty("Exam")]
        [Display(Name = "Soru Sayısı")]
        public virtual ICollection<QuestionExamRelation> QuestionInExam { get; set; }

    }
}