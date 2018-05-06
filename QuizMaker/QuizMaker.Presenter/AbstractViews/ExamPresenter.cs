using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;

namespace QuizMaker.Presenter.AbstractViews
{
    public class ExamPresenter : AbstractExamPresenter
    {
        public ExamPresenter(IBaseView<Exam> view) : base(view)
        {
        }

        public override void AddEntity(TextBox[] textBoxName)
        {
            throw new System.NotImplementedException();
        }
    }
}