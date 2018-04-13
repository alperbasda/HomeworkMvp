using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Abstract
{
    public interface ILessonPresenter
    {
        void ListLessons(Expression<Func<Lesson,bool>> filter = null);
        void GetLesson(Expression<Func<Lesson, bool>> filter = null);
        void AddLesson(TextBox textBoxName);
        void UpdateLesson(TextBox textBoxName);
        void DeleteLesson(string lessonName , int id);

    }
}