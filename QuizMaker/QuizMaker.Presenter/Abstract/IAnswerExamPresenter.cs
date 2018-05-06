
namespace QuizMaker.Presenter.Abstract
{
    public interface IAnswerExamPresenter
    {
        void ShowAnswers(int id);
        void CalculateAnswer(int[,] answers,int examId);

    }
}