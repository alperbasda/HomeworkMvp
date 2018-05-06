using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI.WebControls;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;
using QuizMaker.Presenter.Helpers;

namespace QuizMaker.Presenter.Abstract
{
    public abstract class AbstractExamPresenter
    {
        private IBaseView<Exam> _view;
        protected EfEntityBaseRepository<Exam, QuizMakerContext> Context;
        protected TableBuilder<Exam> TableBuilder = new TableBuilder<Exam>();
        protected AbstractExamPresenter(IBaseView<Exam> view)
        {
            if (view != null)
                _view = view;

            if (Context == null)
                Context = new EfEntityBaseRepository<Exam, QuizMakerContext>();
        }
        public void ListEntityWithTable(Expression<Func<Exam, bool>> filter = null)
        {
            _view.ShowTable(ListToTable(Context.GetList(filter)));
        }

        public void GetEntity(Expression<Func<Exam, bool>> filter = null)
        {
            _view.ShowDetail(Context.Get(filter));
        }
        public StringBuilder ListToTable(IList<Exam> entityList)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<table class='table table-hover table-responsive-md table-fixed'>");
            if (entityList.Count != 0)
            {
                builder.Append(TableBuilder.CreateHeader(entityList[0]));
                builder.Append(TableBuilder.CreateBody(entityList));
            }
            builder.AppendLine("</table>");

            return builder;
        }
        public abstract void AddEntity(TextBox[] textBoxName);
    }
}