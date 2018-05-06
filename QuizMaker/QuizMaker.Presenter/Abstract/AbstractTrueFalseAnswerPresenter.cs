using System.Linq;
using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Abstract
{
    public class AbstractTrueFalseAnswerPresenter : BasePresenter<AnswerTrueFalseTest>, IAnswerPresenter
    {
        public AbstractTrueFalseAnswerPresenter(IBaseView<AnswerTrueFalseTest> view) : base(view)
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


        public void AddAnswer(TextBox[] textBoxs, int questionId)
        {
            foreach (TextBox textBox in textBoxs)
            {
                AnswerTrueFalseTest answer = new AnswerTrueFalseTest
                {
                    QuestionId = questionId,
                    AnswerCount = 0,
                    TrueAnswerCount = 0,
                    MyAnswer = textBox.Text,
                    IsTrue = textBox.ID.ToLower().Split('-').Last() == "true",
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