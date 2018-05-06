using System.Web.UI.WebControls;

namespace QuizMaker.Presenter.Abstract
{
    public interface IExamViewPresenter
    {
        void CreateExam(TextBox[] textBoxs);
    }
}