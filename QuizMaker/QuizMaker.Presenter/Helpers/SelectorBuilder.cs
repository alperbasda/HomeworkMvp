using System.Collections.Generic;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.Presenter.Helpers
{
    public class SelectorBuilder
    {
        private readonly EfEntityBaseRepository<Lesson, QuizMakerContext> _lessonRep;
        public SelectorBuilder()
        {
            if (_lessonRep==null)
            {
                _lessonRep = new EfEntityBaseRepository<Lesson, QuizMakerContext>();
            }
        }
        public string[,] LessonSelector()
        {
            List<Lesson> lessons = _lessonRep.GetList();
            string[,] turnArray=new string[lessons.Count, 2];
            int iterator = 0;
            foreach (var lesson in lessons)
            {
                turnArray[iterator,0]= lesson.Id.ToString();
                turnArray[iterator, 1] = lesson.LessonName;
                iterator++;
            }
            return turnArray;
        }

        public string[] QuestionDifficultySelector()
        {
            
            return new Question().GetQuestionDifficulties();
        }

        public string[] QuestionTypeSelector()
        {
            return new Question().GetQuestionTypes();
        }
    }
}