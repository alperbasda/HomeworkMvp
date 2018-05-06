using System.Collections.Generic;
using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Abstract
{
    public abstract class AbstractQuestionPresenter : BasePresenter<Question>
    {
        protected AbstractQuestionPresenter(IBaseView<Question> view) : base(view)
        {
        }
        public abstract string[] GetQuestionTypesSelector();
        public abstract string[] GetQuestionDifficultiesSelector();
        public abstract string[,] GetLessonsSelector();
        public abstract List<int> CreateOtoExam(TextBox[] textBoxs);
    }
}