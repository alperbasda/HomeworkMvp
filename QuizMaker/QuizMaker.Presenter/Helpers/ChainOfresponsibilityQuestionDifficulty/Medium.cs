using QuizMaker.Entities.Concrete;

namespace QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionDifficulty
{
    public class Medium: CORHandelerBase<DifficultyType>
    {
        public override DifficultyType Handle(int value)
        {
            if (value==1)
                return DifficultyType.Orta;

            if (Succesor!=null)
                return Succesor.Handle(value);

            throw new System.NotImplementedException();
        }
    }
}