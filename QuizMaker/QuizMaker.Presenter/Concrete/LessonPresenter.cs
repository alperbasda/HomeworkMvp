using System;
using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Concrete
{
    public class LessonPresenter :AbstractLessonPresenter
    {
        public LessonPresenter(ILessonView view) 
            : base(view)
        {
        }

        public override void DeleteEntity(int id)
        {
            Context.Delete(Context.Get(s => s.Id == id));
        }

        public override void AddEntity(TextBox[] textBoxName)
        {
            View.ShowDetail(Context.Add(new Lesson() { LessonName = textBoxName[0].Text}));
        }

        public override string AddEntityAjax(TextBox[] textBoxName)
        {
            return TableBuilder.CreateRowAjax(Context.Add(new Lesson() {LessonName = textBoxName[0].Text}));
        }

        public override void UpdateEntity(TextBox[] textBoxName)
        {

            Context.Update(new Lesson() {LessonName = textBoxName[0].Text, Id = Convert.ToInt32(textBoxName[0].ID)});
            ListEntityWithTable();
        }

    }
}