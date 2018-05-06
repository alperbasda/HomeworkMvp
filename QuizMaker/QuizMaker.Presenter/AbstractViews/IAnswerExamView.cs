using System.Text;

namespace QuizMaker.Presenter.AbstractViews
{
    public interface IAnswerExamView
    {
        void ShowAnswer(StringBuilder buildedString);
    }
}