using API.Context;
using API.Models;
using System;

namespace API.Repositories.Data
{
    public class QuizRepository : GeneralRepository<MyContext, Quiz, int>
    {
        private readonly MyContext myContext;
        public QuizRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int InsertQuiz(QuizPostVM entity)
        {
            var quiz = new Quiz
            {
                SubContentId = entity.SubContentId,
                Question = entity.Question

            };
            myContext.Add(quiz);
            myContext.SaveChanges();
            var answer = new Answer
            {
                QuizId = quiz.Id,
                Contents = entity.Answer1,
                IsCorrect = entity.IsCorrect1
            };
            myContext.Add(answer);
            answer = new Answer
            {
                QuizId = quiz.Id,
                Contents = entity.Answer2,
                IsCorrect = entity.IsCorrect2
            };
            myContext.Add(answer);
            answer = new Answer
            {
                QuizId = quiz.Id,
                Contents = entity.Answer3,
                IsCorrect = entity.IsCorrect3
            };
            myContext.Add(answer);
            answer = new Answer
            {
                QuizId = quiz.Id,
                Contents = entity.Answer4,
                IsCorrect = entity.IsCorrect4
            };
            myContext.Add(answer);

            return myContext.SaveChanges();
        }
    }
}
