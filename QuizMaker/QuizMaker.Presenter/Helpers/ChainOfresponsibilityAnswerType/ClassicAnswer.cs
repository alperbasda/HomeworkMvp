using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityAnswerType
{
    public class ClassicAnswer : CORHandelerBase<IAnswerPresenter>
    {
        //yeni soru tipi geldiğinde burada tanımlama yapılabilir
        public ClassicAnswer()
        {
            TrueFalseTestAnswer tft = new TrueFalseTestAnswer();
            SetSuccesor(tft);
        }
        public override IAnswerPresenter Handle(int value)
        {
            if (value == 0)
                return new AbstractClassicAnswerPresenter(null);

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}