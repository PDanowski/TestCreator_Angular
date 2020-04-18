using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
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
