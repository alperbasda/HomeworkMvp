using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;
using QuizMaker.Presenter.Helpers;
using QuizMaker.Presenter.Helpers.ChainOfresponsibilityAnswerType;
using QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionDifficulty;
using QuizMaker.Presenter.Helpers.ChainOfresponsibilityQuestionType;

namespace QuizMaker.Presenter.Concrete
{
    public class QuestionPresenter : AbstractQuestionPresenter
    {
        protected IQuestionView _view;
        private SelectorBuilder _builder;
        public QuestionPresenter(IQuestionView view)
            : base(view)
        {
            
            _view = view;
            if (_builder == null)
            {
                _builder = new SelectorBuilder();
            }
        }

        public override string[] GetQuestionTypesSelector()
        {
            return _builder.QuestionTypeSelector();
        }

        public override string[] GetQuestionDifficultiesSelector()
        {
            return _builder.QuestionDifficultySelector();
        }

        public override string[,] GetLessonsSelector()
        {
            return _builder.LessonSelector();
        }

        public override void DeleteEntity(int id)
        {
            Context.Delete(Context.Get(s => s.Id == id));
        }

        public override void AddEntity(TextBox[] textBoxName)
        {
            Easy easy = new Easy();
            Classic classic = new Classic();
            ClassicAnswer classicAnswer = new ClassicAnswer();
            Question question = new Question();
            IAnswerPresenter answer = null;
            TextBox[] answerTextBoxs = textBoxName.Where(s => s.ID.ToLower().Contains("answer")).ToArray();
            int lid = 0;
            foreach (TextBox textBox in textBoxName)
            {
                if (!textBox.ID.ToLower().Contains("answer"))
                {
                    if (textBox.ID.ToLower().Contains("points"))
                    {
                        decimal d = Convert.ToDecimal(textBox.Text.Replace(".", ","));
                        question.Points = d;
                    }
                    else if (textBox.ID.ToLower().Contains("question"))
                    {
                        question.MyQuestion = textBox.Text;
                    }
                    else if (textBox.ID.ToLower().Contains("selectdifficulty"))
                    {
                        question.Difficulty = easy.Handle(Convert.ToInt32(textBox.Text));
                    }
                    else if (textBox.ID.ToLower().Contains("selecttype"))
                    {
                        question.Type = classic.Handle(Convert.ToInt32(textBox.Text));
                        answer = classicAnswer.Handle((int)question.Type);
                    }
                    else if (textBox.ID.ToLower().Contains("selectlesson"))
                    {
                        lid = Convert.ToInt32(textBox.Text);
                        question.LessonId =lid;
                    }
                }
            }

            
            Context.Add(question);
            answer?.AddAnswer(answerTextBoxs, question.Id);
            ListEntityWithCollapsibleTable();
        }


        public override void UpdateEntity(TextBox[] textBoxName)
        {
            int qid = Convert.ToInt32(textBoxName.FirstOrDefault(s => s.ID == "id")?.Text);
            Question question = Context.Get(s => s.Id == qid);

            Easy easy = new Easy();
            Classic classic = new Classic();
            ClassicAnswer classicAnswer = new ClassicAnswer();
            IAnswerPresenter answer = null;
            TextBox[] answerTextBoxs = textBoxName.Where(s => s.ID.ToLower().Contains("answer")).ToArray();
            foreach (TextBox textBox in textBoxName)
            {
                if (!textBox.ID.ToLower().Contains("answer"))
                {
                    if (textBox.ID.ToLower().Contains("points"))
                    {
                        decimal d = Convert.ToDecimal(textBox.Text.Replace(".", ","));
                        question.Points = d;
                    }
                    else if (textBox.ID.ToLower().Contains("question"))
                    {
                        question.MyQuestion = textBox.Text;
                    }
                    else if (textBox.ID.ToLower().Contains("selectdifficulty"))
                    {
                        question.Difficulty = easy.Handle(Convert.ToInt32(textBox.Text));
                    }
                    else if (textBox.ID.ToLower().Contains("selecttype"))
                    {
                        question.Type = classic.Handle(Convert.ToInt32(textBox.Text));
                        answer = classicAnswer.Handle((int)question.Type);
                    }
                    else if (textBox.ID.ToLower().Contains("selectlesson"))
                    {
                        question.LessonId = Convert.ToInt32(textBox.Text);
                    }
                }
            }
            Context.Update(question);
            if (answerTextBoxs.Length > 0)
                answer?.UpdateAnswer(answerTextBoxs, question.Id);

            ListEntityWithCollapsibleTable();
        }

        public override List<int> CreateOtoExam(TextBox[] textBoxs)
        {
            Question q = new Question();
            QuestionSelector selector = new QuestionSelector();
            int lid = Convert.ToInt32(textBoxs[0].Text);
            int[] counts = { !string.IsNullOrEmpty(textBoxs[3].Text) ? Convert.ToInt32(textBoxs[3].Text) : 0, !string.IsNullOrEmpty(textBoxs[4].Text) ? Convert.ToInt32(textBoxs[4].Text) : 0, !string.IsNullOrEmpty(textBoxs[5].Text) ? Convert.ToInt32(textBoxs[5].Text) : 0 };
            List<int> questionIds=new List<int>();
            int iterator = 0;
            double[] diff= ExamPercentage(q.GetDifficultyByInt(Convert.ToInt32(textBoxs[1].Text)));
            double deger = 0;
            foreach (QuestionType type in Enum.GetValues(typeof(QuestionType)))
            {
                if (counts[iterator] != 0)
                {
                    deger = counts[iterator] * diff[0];
                    selector.QuestionSelect(questionIds, (int) Math.Round(deger), lid, type, DifficultyType.Zor);
                    deger = counts[iterator] * diff[1];
                    selector.QuestionSelect(questionIds, (int) Math.Round(deger), lid, type,DifficultyType.Orta);
                    deger = counts[iterator] * diff[2];
                    selector.QuestionSelect(questionIds, (int)Math.Round(deger), lid, type, DifficultyType.Kolay);
                }
                iterator++;
            }

            return questionIds;
        }

        private double[] ExamPercentage(DifficultyType type)
        {
            switch (type)
            {
                case DifficultyType.Kolay:
                    return new[] { 0.2, 0.3, 0.5 };
                case DifficultyType.Orta:
                    return new[] { 0.2, 0.5, 0.3 };
            }
            return new[] { 0.5, 0.3, 0.2 };
        }
    }
}
