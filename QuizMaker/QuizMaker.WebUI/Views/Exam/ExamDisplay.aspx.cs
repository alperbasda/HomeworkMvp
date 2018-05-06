using System;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Concrete;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace QuizMaker.WebUI.Views.Exam
{
    public partial class ExamDisplay : System.Web.UI.Page, ICreateExamView
    {
        private static IExamViewPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (_presenter == null)
            {
                _presenter = new ExamViewPresenter(this);
            }
            if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "btnCreateExam")
            {
                _presenter.CreateExam(FillFormArray());
            }
            else if (Request.QueryString["ExamId"]!=null)
            {
                ShowExam();
            }
        }

        public void ShowExam()
        {
            string filePath = Server.MapPath(@"~\Contents\image\" + Request.QueryString["ExamId"] + ".pdf");
            WebClient user = new WebClient();
            Byte[] fileBuffer = user.DownloadData(filePath);
            if (fileBuffer != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", fileBuffer.Length.ToString());
                Response.BinaryWrite(fileBuffer);
            }
        }

        public void ShowCreatedExam(StringBuilder buildedString, string examId)
        {

            BaseFont STF_Helvetica_Turkish = BaseFont.CreateFont("Helvetica", "CP1254", BaseFont.NOT_EMBEDDED);

            Font fontNormal = new Font(STF_Helvetica_Turkish, 12, Font.NORMAL);
            Document pdf = new Document(PageSize.A4, 90, 75, 65, 15);

            PdfWriter.GetInstance(pdf, new FileStream(Server.MapPath(@"~\Contents\image\" + examId + ".pdf"), FileMode.Create));
            if (pdf.IsOpen() == false)
            {
                pdf.Open();
            }
            //pdfDosya.AddCreator(textBox2.Text); //Oluşturan kişinin isminin eklenmesi

            //pdfDosya.AddCreationDate();//Oluşturulma tarihinin eklenmesi

            //pdfDosya.AddAuthor(textBox3.Text); //Yazarın isiminin eklenmesi

            //pdfDosya.AddHeader(textBox4.Text, "PDF UYGULAMASI OLUSTUR");

            //pdfDosya.AddTitle(textBox5.Text); //Başlık ve title eklenmesi
            pdf.Add(new Paragraph(buildedString.ToString(), fontNormal));
            pdf.Close();
            string filePath = Server.MapPath(@"~\Contents\image\" + examId + ".pdf");
            WebClient user = new WebClient();
            Byte[] fileBuffer = user.DownloadData(filePath);
            if (fileBuffer != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", fileBuffer.Length.ToString());
                Response.BinaryWrite(fileBuffer);
            }
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