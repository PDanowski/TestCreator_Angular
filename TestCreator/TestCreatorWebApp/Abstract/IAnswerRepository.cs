using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
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
