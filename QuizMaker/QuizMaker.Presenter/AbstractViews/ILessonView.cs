using System.Collections.Generic;
using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.AbstractViews
{

    public interface ILessonView
    {
        void GetLessonPage(IList<Lesson> lessons);
        void GetLessonDetail(Lesson lesson);
        string LessonListToString(IList<Lesson> lessons);
    }
}