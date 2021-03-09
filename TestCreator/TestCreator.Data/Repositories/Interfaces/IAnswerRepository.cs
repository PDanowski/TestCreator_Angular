using System.Collections.Generic;
using TestCreator.Data.Models;

namespace TestCreator.Data.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Answer GetAnswer(int id);
        List<Answer> GetAnswers(int questionId);
        Answer CreateAnswer(Answer answer);
        Answer UpdateAnswer(Answer answer);
        bool DeleteAnswer(int id);
    }
}
