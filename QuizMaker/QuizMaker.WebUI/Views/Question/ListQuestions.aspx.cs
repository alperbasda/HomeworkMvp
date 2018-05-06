using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.WebUI.Views.Question
{
    public partial class ListQuestions : System.Web.UI.Page, IQuestionView
    {
        private static AbstractQuestionPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new QuestionPresenter(this);
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
            else if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "btnCreateExam")
            {
                SendQueryStringExam();
            }
            else if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "btnCreate")
            {
                CreateQuestion();
            }
            _presenter.ListEntityWithCollapsibleTable();
            FillSelectors();
        }


        public void ShowTable(StringBuilder buildedString)
        {
            tableItems.InnerHtml = buildedString.ToString();
        }

        public void ShowDetail(Entities.Concrete.Question entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity()
        {
            _presenter.DeleteEntity(Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));
            Response.Redirect(Request.RawUrl);
        }

        public void SendQueryStringExam()
        {
            List<int> questinIds = _presenter.CreateOtoExam(new[]
            {
                new TextBox() {ID = "LessonId", Text = Request.Form["ders"]},
                new TextBox() {ID = "Difficulty", Text = Request.Form["zorluk"]},
                new TextBox() {ID = "Type", Text = Request.Form["tip"]},
                new TextBox() {ID = "KQuestionCount", Text = Request.Form["ksay"]},
                new TextBox() {ID = "tQuestionCount", Text = Request.Form["tsay"]},
                new TextBox() {ID = "DQuestionCount", Text = Request.Form["dsay"]}
            });
            questinIds.ForEach(s => otoexam.Value += s + ",");
        }

        public void EditEntity()
        {
            TextBox[] lisTextBoxs = new TextBox[Request.Form.Keys.Count - 4];
            lisTextBoxs[0] = new TextBox() { ID = "id", Text = Request.Form["__EVENTARGUMENT"] };
            int i = 1;
            foreach (var textBox in FillFormArray())
            {
                lisTextBoxs[i] = textBox;
                i++;
            }
            _presenter.UpdateEntity(lisTextBoxs);
            Response.Redirect(Request.RawUrl);
        }

        public void FillSelectors()
        {
            int valAdder = 0;
            if (TypeSelector.Items.Count <= 1)
            {

                foreach (var item in _presenter.GetQuestionTypesSelector())
                {
                    TypeSelector.Items.Add(new ListItem() { Text = item, Value = valAdder.ToString() });
                    selectType.Items.Add(new ListItem() { Text = item, Value = valAdder.ToString() });
                    valAdder++;
                }

                valAdder = 0;
                foreach (var item in _presenter.GetQuestionDifficultiesSelector())
                {
                    DifficultySelector.Items.Add(new ListItem() { Text = item, Value = valAdder.ToString() });
                    selectDifficulty.Items.Add(new ListItem() { Text = item, Value = valAdder.ToString() });
                    valAdder++;
                }

                string[,] responseArray = _presenter.GetLessonsSelector();
                for (int i = 0; i < responseArray.GetLength(0); i++)
                {
                    LessonSelector.Items.Add(new ListItem() { Value = responseArray[i, 0], Text = responseArray[i, 1] });
                    selectLesson.Items.Add(new ListItem() { Value = responseArray[i, 0], Text = responseArray[i, 1] });
                }

            }
        }

        public void CreateQuestion()
        {
            _presenter.AddEntity(FillFormArray());
            Response.Redirect(Request.RawUrl);
        }

        private TextBox[] FillFormArray()
        {
            TextBox[] dizi = new TextBox[Request.Form.Keys.Count - 5];
            int i = 0;
            foreach (string key in Request.Form.Keys)
            {
                if (key.Contains("__")) continue;
                dizi[i] = new TextBox() { ID = key, Text = Request.Form[key] };
                i++;
            }
            return dizi;
        }
    }
}