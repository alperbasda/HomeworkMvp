using System;
using System.Text;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.WebUI.Views.Lesson
{
    public partial class ListLessons : System.Web.UI.Page, ILessonView
    {
        private static AbstractLessonPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new LessonPresenter(this);
            if (!Page.IsPostBack)
            {

            }
            else if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "btnDelete")
            {
                DeleteEntity();
            }
            else if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "btnEdit")
            {
                EditEntity();
            }
            _presenter.ListEntityWithTable();
        }


        public void ShowTable(StringBuilder buildedString)
        {
            tableItems.InnerHtml = buildedString.ToString();
        }

        public void ShowDetail(Entities.Concrete.Lesson entity)
        {
            throw new NotImplementedException();
        }



        public void DeleteEntity()
        {
            _presenter.DeleteEntity(Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));
            //Presenter katmanımdan ShowTable çağırdıgımda form postback oluyor ve buda form gönderimini tekrar tetiklediği için 
            //kullanıcıya uyarı vermesin diye redirect atıyoruz
            Response.Redirect(Request.RawUrl);

        }

        public void EditEntity()
        {
            _presenter.UpdateEntity(new[] { new TextBox() { Text = Request.Form["lessonName"], ID = Request.Form["__EVENTARGUMENT"] } });
            Response.Redirect(Request.RawUrl);
        }

        //Bu Kodu Yazmış bulundum asp.net te static tanımlama yapmak gerekiyormus ajax çağrısı için cok sacma
        //Diğerlerinde sayfayı refresh edicem
        [System.Web.Services.WebMethod]
        public static string CreateLesson(string name)
        {
            return _presenter.AddEntityAjax(new[] { new TextBox() { Text = name } });
        }


    }
}