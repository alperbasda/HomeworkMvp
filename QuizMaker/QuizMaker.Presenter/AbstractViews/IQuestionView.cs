using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.AbstractViews
{
    public interface IQuestionView:IBaseView<Question>
    {
        void FillSelectors();
        void CreateQuestion();
    }
}