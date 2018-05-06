using System;
using System.Collections.Generic;
using QuizMaker.Core.AbstractEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizMaker.Entities.Concrete
{
    public enum QuestionType
    {
        Klasik,
        Test,
        DogruYanlis
    }
    public enum DifficultyType
    {
        Zor,
        Orta,
        Kolay
    }

    public class Question : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Soru")]
        public string MyQuestion { get; set; }
        [Display(Name = "Puan")]
        public decimal Points { get; set; }
        [Display(Name = "Tip")]
        public QuestionType Type { get; set; }
        [Display(Name = "Zorluk")]
        public DifficultyType Difficulty { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        public int LessonId { get; set; }

        [NotMapped]
        [Display(Name = "Ders Adı")]
        public string LessonName => Lesson.LessonName;

        [InverseProperty("Question")]
        [Display(Name = "Kullanılma Sayısı")]
        public virtual ICollection<QuestionExamRelation> ContainsExams { get; set; }

        public virtual ICollection<AnswerClassic> ContainsAnswerClassics { get; set; }

        public virtual ICollection<AnswerTrueFalseTest> ContainsAnswerTests { get; set; }

        public string[] GetQuestionTypes()
        {
            return Enum.GetNames(typeof(QuestionType));
        }

        public string[] GetQuestionDifficulties()
        {
            return Enum.GetNames(typeof(DifficultyType));
        }

        public DifficultyType GetDifficultyByInt(int typeId)
        {
            switch (typeId)
            {
                case 0:
                    return DifficultyType.Zor;
                case 1:
                    return DifficultyType.Orta;
                case 2:
                    return DifficultyType.Kolay;
                default:
                    return DifficultyType.Kolay;
            }
        }
        public QuestionType GetTypeByInt(int typeId)
        {
            switch (typeId)
            {
                case 0:
                    return QuestionType.Klasik;
                case 1:
                    return QuestionType.Test;
                case 2:
                    return QuestionType.DogruYanlis;
                default:
                    return QuestionType.Klasik;
            }
        }



    }
}