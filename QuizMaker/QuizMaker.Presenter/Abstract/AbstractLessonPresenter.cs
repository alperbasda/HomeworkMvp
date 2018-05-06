using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Abstract
{
    public abstract class AbstractLessonPresenter:BasePresenter<Lesson>
    {
        protected AbstractLessonPresenter(IBaseView<Lesson> view) 
            : base(view)
        {
        }

        public abstract string AddEntityAjax(TextBox[] textBoxName);
    }
}