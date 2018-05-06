using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionDifficulty
{
    public class Hard : CORHandelerBase<DifficultyType>
    {
        public override DifficultyType Handle(int value)
        {
            if (value == 0)
                return DifficultyType.Zor;

            if (Succesor != null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}