using System.Collections.Generic;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Abstract
{
    public interface IAnswerRepository
    {
        AnswerViewModel GetAnswer(int id);
        List<AnswerViewModel> GetAnswers(int questionId);
        AnswerViewModel CreateAnswer(AnswerViewModel viewModel);
        AnswerViewModel UpdateAnswer(AnswerViewModel viewModel);
        bool DeleteAnswer(int id);
    }
}
