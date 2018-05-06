using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Helpers;

namespace QuizMaker.Presenter.Concrete
{
    public class ExamViewPresenter : IExamViewPresenter
    {
        private ICreateExamView _view;
        private ExamBuilder _builder;
        private EfEntityBaseRepository<Question, QuizMakerContext> _context;
        public ExamViewPresenter(ICreateExamView view)
        {
            _context= new EfEntityBaseRepository<Question, QuizMakerContext>();
            _view = view;
        }
        public void CreateExam(TextBox[] textBoxs)
        {
            _builder = new ExamBuilder(textBoxs[textBoxs.Length - 2].Text);
            List<Question> questions = new List<Question>();
            int cast;
            for (int i = 0; i < textBoxs.Length-2; i++)
            {
                cast = Convert.ToInt32(textBoxs[i].ID);
                questions.Add(_context.Get(s => s.Id == cast));
            }

            Exam e = _builder.SetLesson(questions[0].Lesson)
                .SetObserver(textBoxs[textBoxs.Length - 1].Text).AddQuestions(questions).Build();
            _view.ShowCreatedExam(_builder.ConvertExamToString(e,questions),e.Id.ToString());
        }
        
    }
}