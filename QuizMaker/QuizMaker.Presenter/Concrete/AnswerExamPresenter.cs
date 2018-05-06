using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Abstract;
using QuizMaker.Presenter.AbstractViews;

namespace QuizMaker.Presenter.Concrete
{
    public class AnswerExamPresenter : IAnswerExamPresenter
    {
        private EfEntityBaseRepository<Exam, QuizMakerContext> _context;
        private EfEntityBaseRepository<AnswerClassic, QuizMakerContext> _contextClassic;
        private EfEntityBaseRepository<AnswerTrueFalseTest, QuizMakerContext> _contextTest;
        private IAnswerExamView _view;
        private readonly char[] _alfabeArray;
        public AnswerExamPresenter(IAnswerExamView view)
        {
            if (_view==null)
            {
                _view = view;
            }
            if (_alfabeArray == null)
            {
                _alfabeArray = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };
            }
            if (_context == null)
            {
                _context = new EfEntityBaseRepository<Exam, QuizMakerContext>();
                _contextClassic = new EfEntityBaseRepository<AnswerClassic, QuizMakerContext>();
                _contextTest = new EfEntityBaseRepository<AnswerTrueFalseTest, QuizMakerContext>();
            }
        }
        public void ShowAnswers(int id)
        {
            Exam exam = _context.Get(s => s.Id == id);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h2>" + exam.LessonName + "</h2><br/>");
            sb.AppendLine("<h3>" + exam.Name + "</h3><br/><h3> " + exam.Observer + " </h3><br/><br/>");
            int alfabeIterator = 0, iterateTrueAnswer = 0, iterator = 0, iteratorAnswer = 0;
            List<int> trueAnswers;
            

            foreach (QuestionExamRelation question in exam.QuestionInExam)
            {
                trueAnswers = new List<int>();
                sb.AppendLine("<label><b>" + (++iterator) + " -) " + question.Question.MyQuestion + "</b></label></br>");
                foreach (var classic in question.Question.ContainsAnswerClassics)
                {
                    sb.AppendLine("<label><i>Cevap : " + classic.MyAnswer + "</i></label></br>");
                    sb.AppendLine("<br/><label>Puanı : &nbsp<input name='classicAnswer" + classic.Id + "' id='classicAnswer" + classic.Id + "' type='text'></label><br/><br/>");
                    iteratorAnswer++;
                }
                foreach (var test in question.Question.ContainsAnswerTests)
                {    
                    if (test.IsTrue)
                    {
                        sb.AppendLine("<label style='color:green'><i>" + _alfabeArray[alfabeIterator].ToString() + "-)" + test.MyAnswer + "</i></label><br/>");
                        trueAnswers.Add(test.Id);
                        iteratorAnswer++;
                    }
                    else
                    {
                        sb.AppendLine("<label>" + _alfabeArray[alfabeIterator].ToString() + "-)" + test.MyAnswer + "</label><br/>");
                    }

                    alfabeIterator++;
                    
                }

                if (question.Question.Type!=QuestionType.Klasik)
                {
                    string trueAnswersstr = "";
                    foreach (int trueAnswer in trueAnswers)
                    {
                        trueAnswersstr += trueAnswer + "-";
                    }
                    sb.AppendLine("<br/><label>Doğru mu &nbsp<input name='" + trueAnswersstr + "' id='" + trueAnswersstr + "' type='checkbox'></label><br/><br/>");
                }
                alfabeIterator = 0;
            }
            sb.AppendLine("<input type='hidden' name='answerCount' id='answerCount' value='"+iteratorAnswer + "' type='checkbox'>");
            _view.ShowAnswer(sb);
            
        }

        public void CalculateAnswer(int[,] answers,int examId)
        {
            int[] ids = new int[answers.GetLength(0)];
            for (int i = 0; i < answers.GetLength(0); i++)
            {
                if (answers[i, 1] ==-1)
                {
                    ids[i] = answers[i, 0];
                }
            }
            Exam exam = _context.Get(s => s.Id == examId);
            exam.QuestionInExam.ToList().ForEach(s=>s.Question.ContainsAnswerTests.ToList().ForEach(k=>k.AnswerCount+=1));
            exam.QuestionInExam.ToList().ForEach(s => s.Question.ContainsAnswerTests
                .Where(r=>ids.Contains(r.Id)).Where(k => k.IsTrue)
                .ToList().ForEach(q=>q.TrueAnswerCount+=1));
            
            for (int i = 0; i < answers.GetLength(0); i++)
            {
                int lastId = answers[i, 0];
                if (answers[i,1]!=-1)
                {
                    AnswerClassic classic = _contextClassic.Get(s => s.Id == lastId);
                    if (classic.Highest <= answers[i, 1])
                    {
                        classic.Highest = answers[i, 1];
                    }
                    if (classic.Lowest >= answers[i, 1])
                    {
                        classic.Lowest = answers[i, 1];
                    }
                    classic.Avarage = ((classic.Avarage * classic.AnswerCount) + answers[i, 1])/ ++classic.AnswerCount;
                    _contextClassic.Update(classic);
                }
                
            }
            _context.Update(exam);
            
        }

        
    }
}