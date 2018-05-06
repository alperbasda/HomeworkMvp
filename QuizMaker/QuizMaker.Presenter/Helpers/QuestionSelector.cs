using System;
using System.Collections.Generic;
using System.Linq;
using QuizMaker.Core.EntityFramework;
using QuizMaker.Entities.Concrete;
using QuizMaker.Presenter.Concrete;

namespace QuizMaker.Presenter.Helpers
{
    public class QuestionSelector
    {
        private readonly EfEntityBaseRepository<Question, QuizMakerContext> _context;
        public QuestionSelector()
        {
            _context = new EfEntityBaseRepository<Question, QuizMakerContext>();
        }
        public List<int> QuestionSelect(List<int> questionIds, int count, int lessonId, DifficultyType difficulty)
        {
            int tableCount = _context.GetTableCount(s => s.LessonId == lessonId && s.Difficulty == difficulty);
            if (tableCount < count)
            {
                _context.GetList(s => s.LessonId == lessonId && s.Difficulty == difficulty).Select(s => s.Id).ToList().ForEach(questionIds.Add);
                questionIds.Add(-1);
                return questionIds;
            }
            var randomNumberGenerator = new Random();
            List<int> skipValues = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomValue = randomNumberGenerator.Next(tableCount);
                repeat: if (skipValues.Any(s => s == randomValue))
                {
                    randomValue = randomNumberGenerator.Next(tableCount);
                    goto repeat;
                }

                skipValues.Add(randomValue);
                questionIds.Add(_context.Get(s => s.LessonId == lessonId && s.Difficulty == difficulty, randomValue).Id);


            }

            return questionIds;

        }

        public List<int> QuestionSelect(List<int> questionIds, int count, int lessonId, QuestionType type, DifficultyType difficulty)
        {
            int tableCount = _context.GetTableCount(s => s.LessonId == lessonId && s.Difficulty == difficulty && s.Type == type);
            if (tableCount < count)
            {
                _context.GetList(s => s.LessonId == lessonId && s.Difficulty == difficulty && s.Type == type).Select(s => s.Id).ToList().ForEach(questionIds.Add);
                questionIds.Add(-1);
                return questionIds;
            }
            var randomNumberGenerator = new Random();
            List<int> skipValues = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomValue = randomNumberGenerator.Next(tableCount);
                repeat: if (skipValues.Any(s => s == randomValue))
                {
                    randomValue = randomNumberGenerator.Next(tableCount);
                    goto repeat;
                }

                skipValues.Add(randomValue);
                questionIds.Add(_context.Get(s => s.LessonId == lessonId && s.Difficulty == difficulty && s.Type == type, randomValue).Id);

            }

            return questionIds;

        }

    }
}