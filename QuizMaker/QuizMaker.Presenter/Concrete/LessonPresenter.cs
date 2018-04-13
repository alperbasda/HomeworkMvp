using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Concrete
{
    public class LessonPresenter : EfEntityBaseRepository<Lesson, QuizMakerContext>, ILessonPresenter
    {
        ILessonView _view;

        public LessonPresenter(ILessonView view)
        {
            _view = view;
        }


        public void ListLessons(Expression<Func<Lesson, bool>> filter = null)
        {
            _view.GetLessonPage(GetList(filter));
        }

        public void GetLesson(Expression<Func<Lesson, bool>> filter = null)
        {
            _view.GetLessonDetail(Get(filter));
        }

        public void AddLesson(TextBox textBoxName)
        {
           _view.GetLessonDetail(Add(new Lesson() {LessonName = textBoxName.Text, Id = Convert.ToInt32( textBoxName.ID) }));
        }

        public void UpdateLesson(TextBox textBoxName)
        {
           _view.GetLessonDetail(Update(new Lesson() { LessonName = textBoxName.Text, Id = Convert.ToInt32(textBoxName.ID) }));
        }

        public void DeleteLesson(string lessonName,int id)
        {
            Delete(new Lesson() { LessonName = lessonName, Id = id });
            _view.GetLessonPage(GetList());
        }
    }
}