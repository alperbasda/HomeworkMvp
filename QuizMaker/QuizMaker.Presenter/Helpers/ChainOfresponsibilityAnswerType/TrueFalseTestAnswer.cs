using QuizMaker.Core.AbstractEntity;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityAnswerType
{
    public class TrueFalseTestAnswer : CORHandelerBase<IAnswerPresenter>
    {
        public override IAnswerPresenter Handle(int value)
        {
            if (value == 1 || value==2)
                return new AbstractTrueFalseAnswerPresenter(null);

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}