using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionDifficulty
{
    public class Easy : CORHandelerBase<DifficultyType>
    {
        public Easy()
        {
            Medium m = new Medium();
            SetSuccesor(m);
            Hard h = new Hard();
            m.SetSuccesor(h);
        }
        public override DifficultyType Handle(int value)
        {
            if (value==2)
                return DifficultyType.Kolay;

            if(Succesor!=null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}