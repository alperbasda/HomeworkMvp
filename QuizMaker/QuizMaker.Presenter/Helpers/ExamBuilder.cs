using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.Presenter.Helpers
{
    public class ExamBuilder
    {
        private Exam _exam;
        private static List<Question> _questions;
        private static EfEntityBaseRepository<Exam, QuizMakerContext> _examrepo;
        private static EfEntityBaseRepository<QuestionExamRelation, QuizMakerContext> _relationrepo;
        private Lesson _lesson;
        private static char[] _alfabeArray;
        public ExamBuilder(string examName)
        {
            _exam = new Exam { Name = examName };
            if (_examrepo == null)
            {
                _examrepo = new EfEntityBaseRepository<Exam, QuizMakerContext>();
                _relationrepo = new EfEntityBaseRepository<QuestionExamRelation, QuizMakerContext>();
            }
            if (_alfabeArray == null)
            {
                _alfabeArray = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };
            }
        }
        public string GetExamName()
        {
            return _exam.Name;
        }

        public List<Question> GetQuestions()
        {
            return _exam.QuestionInExam.Select(s => s.Question).ToList();
        }

        public ExamBuilder AddQuestions(List<Question> questions)
        {
            _questions = questions;
            return this;
        }

        public Exam Build()
        {
            _examrepo.Add(_exam);
            foreach (Question question in _questions)
            {
                _relationrepo.Add(new QuestionExamRelation()
                {
                    ExamId = _exam.Id,
                    QuestionId = question.Id
                });
            }
            return _exam;
        }

        public ExamBuilder SetObserver(string observer)
        {
            _exam.Observer = observer;
            return this;
        }
        public string GetObserver()
        {
            return _exam.Observer;
        }
        public ExamBuilder SetLesson(Lesson lesson)
        {
            _exam.LessonId = lesson.Id;
            _lesson = lesson;
            return this;
        }
        public Lesson GetLesson()
        {
            return _lesson;
        }

        public StringBuilder ConvertExamToString(Exam e,List<Question> questions)
        {
            StringBuilder sb = new StringBuilder();
            decimal d = 0;
            int iterator = 0;
            int alfabeIterator = 0;
            questions.ForEach(s=>d += s.Points);
            sb.AppendLine("Ad : ");
            sb.AppendLine("Soyad : ");
            sb.AppendLine("Numara : ");
            sb.AppendLine(_lesson.LessonName + " Sınavı " + " Sınav " + (int)d + " puan üzerindendir.");
            sb.AppendLine();
            foreach (Question question in questions)
            {
                sb.AppendLine("" +(++iterator)+" -) "+ question.MyQuestion);
                sb.AppendLine();
                foreach (var classic in question.ContainsAnswerClassics)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        sb.AppendLine();
                    }
                }
                foreach (var test in question.ContainsAnswerTests)
                {
                    sb.AppendLine(_alfabeArray[ alfabeIterator].ToString() +"-)" + test.MyAnswer);
                    alfabeIterator++;
                }
                alfabeIterator = 0;
                sb.AppendLine();
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Gözetmen : "+ e.Observer);
            sb.AppendLine("Başarılar !!!");
            return sb;
        }

    }
}