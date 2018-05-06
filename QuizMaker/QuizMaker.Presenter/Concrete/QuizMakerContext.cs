using QuizMaker.Entities.Concrete;
using System.Data.Entity;

namespace QuizMaker.Presenter.Concrete
{
    public class QuizMakerContext:DbContext
    {
        public DbSet<AnswerClassic> AnswerClassics { get; set; }

        public DbSet<AnswerTrueFalseTest> AnswerTrueFalseTests { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionExamRelation> QuestionExamRelations { get; set; }

        public QuizMakerContext()
        :base("QuizMakerDatabase")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}