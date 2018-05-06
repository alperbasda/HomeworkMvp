using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.WebUI.Views.Exam
{
    public partial class CreateExam : System.Web.UI.Page,IExamView
    {
        ExamPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_presenter==null)
            {
                _presenter = new ExamPresenter(this);
            }
            _presenter.ListEntityWithTable();
        }

        public void FillSelectors()
        {
            throw new NotImplementedException();
        }

        public void ShowTable(StringBuilder buildedString)
        {
            tableItems.InnerHtml = buildedString.ToString();
        }

        public void ShowDetail(Entities.Concrete.Exam entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity()
        {
            throw new NotImplementedException();
        }

        public void EditEntity()
        {
            throw new NotImplementedException();
        }
    }
}