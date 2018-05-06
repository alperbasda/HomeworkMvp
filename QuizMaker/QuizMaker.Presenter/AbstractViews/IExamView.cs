using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.AbstractViews
{
    public interface IExamView : IBaseView<Exam>
    {
        void FillSelectors();
    }
}