using System.Collections.Generic;
using TestCreator.Data.Models;

namespace TestCreator.Data.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetQuestion(int id);
        List<Question> GetQuestions(int testId);
        Question CreateQuestion(Question question);
        Question UpdateQuestion(Question question);
        bool DeleteQuestion(int id);
    }
}
