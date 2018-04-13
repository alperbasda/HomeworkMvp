using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Entities.Concrete
{
    public class Lesson :IEntity
    {
        [Key]
        public int Id { get; set; }

        public string LessonName { get; set; }

        public virtual ICollection<Question> QuestionInLesson { get; set; }

        public virtual ICollection<Exam> ExamInLesson { get; set; }

    }
}