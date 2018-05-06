using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionType
{
    public class Test : CORHandelerBase<QuestionType>
    {
        public override QuestionType Handle(int value)
        {
            if (value == 1)
                return QuestionType.Test;

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}