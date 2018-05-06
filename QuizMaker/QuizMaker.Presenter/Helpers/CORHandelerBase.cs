using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers
{
    public abstract class CORHandelerBase<T>
    {
        protected CORHandelerBase<T> Succesor;
        public abstract T Handle(int value);
        public void SetSuccesor(CORHandelerBase<T> succesor)
        {
            Succesor = succesor;
        }
    }
}