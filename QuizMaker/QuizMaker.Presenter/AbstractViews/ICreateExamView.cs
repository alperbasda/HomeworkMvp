using System.Text;

namespace QuizMaker.Presenter.AbstractViews
{
    public interface ICreateExamView
    {
        void ShowCreatedExam(StringBuilder buildedString,string examId);
    }
}