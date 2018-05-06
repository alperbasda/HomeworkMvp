using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.WebUI.Views.Exam
{
    public partial class ExamAnswers : System.Web.UI.Page, IAnswerExamView
    {
        AnswerExamPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_presenter == null)
            {
                _presenter = new AnswerExamPresenter(this);
            }
            if (Request.QueryString["ExamId"] != null)
            {
                Successfull();
            }



        }

        private void Successfull()
        {
            _presenter.ShowAnswers(Convert.ToInt32(Request.QueryString["ExamId"]));
        }

        public void ShowAnswer(StringBuilder buildedString)
        {
            tableItems.InnerHtml = buildedString.ToString();
        }
        private int[,] FillFormArray()
        {
            int[,] dizi = new int[Convert.ToInt32(Request.Form["answerCount"]), 2];
            int i = 0;
            int deger = 0;
            foreach (string key in Request.Form.Keys)
            {
                string[] keys = key.Split('-');
                foreach (var s in keys)
                {
                    if (int.TryParse(s, out deger))
                    {
                        dizi[i, 0] = deger;
                        dizi[i, 1] = -1;
                        i++;
                    }
                }
                
                if (key.Contains("classicAnswer"))
                {
                    dizi[i, 0] =Convert.ToInt32(key.Substring(13));
                    dizi[i, 1] = Convert.ToInt32(Request.Form[key]);
                    i++;
                }
            }
            return dizi;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            _presenter.CalculateAnswer(FillFormArray(),Convert.ToInt32( Request.QueryString["ExamId"]));

        }
    }
}