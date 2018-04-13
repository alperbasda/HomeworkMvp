using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.WebUI.Views.Lesson
{
    public partial class ListLessons : System.Web.UI.Page,ILessonView
    {
        private ILessonPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new LessonPresenter(this);
            _presenter.ListLessons();
        }

        public void GetLessonPage(IList<Entities.Concrete.Lesson> lessons)
        {
            Content.InnerHtml = LessonListToString(lessons);
        }

        public void GetLessonDetail(Entities.Concrete.Lesson lesson)
        {
            throw new NotImplementedException();
        }

        public string LessonListToString(IList<Entities.Concrete.Lesson> lessons)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<ul>");
            foreach (var lesson in lessons)
            {
                builder.AppendLine("<li>"+lesson.LessonName+"</li>");
            }
            builder.AppendLine("</ul>");
            return builder.ToString();
        }
    }
}