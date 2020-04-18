using System.Collections.Generic;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Abstract
{
    public interface IQuestionRepository
    {
        QuestionViewModel GetQuestion(int id);
        List<QuestionViewModel> GetQuestions(int testId);
        QuestionViewModel CreateQuestion(QuestionViewModel viewModel);
        QuestionViewModel UpdateQuestion(QuestionViewModel viewModel);
        bool DeleteQuestion(int id);
    }
}
