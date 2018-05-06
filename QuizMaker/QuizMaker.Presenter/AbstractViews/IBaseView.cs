using System.Text;
using QuizMaker.Core.AbstractEntity;

namespace QuizMaker.Presenter.AbstractViews
{
    public interface IBaseView<T> where T:class,IEntity,new()
    {
        void ShowTable(StringBuilder buildedString);
        void ShowDetail(T entity);
        void DeleteEntity();
        void EditEntity();
    }
}