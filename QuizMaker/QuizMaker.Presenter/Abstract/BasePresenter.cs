using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI.WebControls;
using QuizMaker.Core.AbstractEntity;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;
using QuizMaker.Presenter.Helpers;

namespace QuizMaker.Presenter.Abstract
{
    public abstract class BasePresenter<T>
        where T : class, IEntity, new()
    {
        protected IBaseView<T> View;
        protected EfEntityBaseRepository<T, QuizMakerContext> Context;
        protected TableBuilder<T> TableBuilder = new TableBuilder<T>();
        protected BasePresenter(IBaseView<T> view)
        {
            if (view!=null)
                View = view;

            if (Context == null)
                Context = new EfEntityBaseRepository<T, QuizMakerContext>();
        }
        public void ListEntityWithTable(Expression<Func<T, bool>> filter = null)
        {
            View.ShowTable(ListToTable(Context.GetList(filter)));
        }

        public void ListEntityWithCollapsibleTable(Expression<Func<T, bool>> filter = null)
        {
            View.ShowTable(ListToCollapsibleTable(Context.GetList(filter)));
        }

        public void GetEntity(Expression<Func<T, bool>> filter = null)
        {
            View.ShowDetail(Context.Get(filter));
        }

        public StringBuilder ListToCollapsibleTable(IList<T> entityList)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<table class='table table-hover table-responsive-md table-fixed'>");
            if (entityList.Count != 0)
            {
                builder.Append(TableBuilder.CreateHeader(entityList[0]));
                builder.Append(TableBuilder.CreateBodyCollapsible(entityList));
            }
            builder.AppendLine("</table>");

            return builder;
        }

        public StringBuilder ListToTable(IList<T> entityList)
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
        public abstract void DeleteEntity(int id);
        public abstract void AddEntity(TextBox[] textBoxName);
        public abstract void UpdateEntity(TextBox[] textBoxName);
    }
}