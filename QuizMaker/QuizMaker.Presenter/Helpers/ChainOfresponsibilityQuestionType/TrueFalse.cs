using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionType
{
    public class TrueFalse : CORHandelerBase<QuestionType>
    {
        public override QuestionType Handle(int value)
        {
            if (value == 2)
                return QuestionType.DogruYanlis;

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}