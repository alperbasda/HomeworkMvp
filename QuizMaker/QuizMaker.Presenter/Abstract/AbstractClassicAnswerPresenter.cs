using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Abstract
{
    public class AbstractClassicAnswerPresenter: BasePresenter<AnswerClassic>,IAnswerPresenter
    {
        public AbstractClassicAnswerPresenter(IBaseView<AnswerClassic> view) : base(view)
        {
        }

        public override void DeleteEntity(int id)
        {
            throw new System.NotImplementedException();
        }

        public override void AddEntity(TextBox[] textBoxName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateEntity(TextBox[] textBoxName)
        {
            throw new System.NotImplementedException();
        }

        public void AddAnswer(TextBox[] textBoxs,int questionId)
        {
            foreach (TextBox textBox in textBoxs)
            {
                AnswerClassic answer = new AnswerClassic()
                {
                    IsTrue = true,
                    QuestionId = questionId,
                    MyAnswer = textBox.Text,
                    Avarage = 0,
                    Highest = 0,
                    Lowest = 100,
                    AnswerCount = 0
                };
                Context.Add(answer);
            }
        }

        public void UpdateAnswer(TextBox[] textBoxs, int questionId)
        {
            foreach (var deletedItem in Context.GetList(s => s.QuestionId == questionId))
                Context.Delete(deletedItem);
            
            AddAnswer(textBoxs,questionId);
        }
    }
}