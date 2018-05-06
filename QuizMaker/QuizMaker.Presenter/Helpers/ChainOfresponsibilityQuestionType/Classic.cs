using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionType
{
    public class Classic : CORHandelerBase<QuestionType>
    {
        public Classic()
        {
            Test test = new Test();
            SetSuccesor(test);
            TrueFalse trueFalse = new TrueFalse();
            test.SetSuccesor(trueFalse);
        }
        public override QuestionType Handle(int value)
        {
            if (value == 0)
                return QuestionType.Klasik;

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}