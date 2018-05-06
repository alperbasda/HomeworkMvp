using System.Web.UI.WebControls;

namespace QuizMaker.Presenter.Abstract
{
    public interface IAnswerPresenter
    {
        void AddAnswer(TextBox[] textBoxs,int questionId);
        void UpdateAnswer(TextBox[] textBoxs, int questionId);
    }
}