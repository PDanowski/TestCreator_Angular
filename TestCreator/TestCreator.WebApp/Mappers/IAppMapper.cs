using System.Collections.Generic;
using TestCreator.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Mappers
{
    public interface IAppMapper
    {
        AnswerViewModel ToViewModel(Answer source);
        List<AnswerViewModel> ToViewModels(List<Answer> source);
        Answer ToModel(AnswerViewModel source);

        QuestionViewModel ToViewModel(Question source);
        List<QuestionViewModel> ToViewModels(List<Question> source);
        Question ToModel(QuestionViewModel source);

        ResultViewModel ToViewModel(Result source);
        List<ResultViewModel> ToViewModels(List<Result> source);
        Result ToModel(ResultViewModel source);

        TestViewModel ToViewModel(Test source);
        List<TestViewModel> ToViewModels(List<Test> source);
        Test ToModel(TestViewModel source);

        UserViewModel ToViewModel(ApplicationUser source);
        ApplicationUser ToModel(UserViewModel source);

        TestAttemptAnswerViewModel ToAttemptAnswerViewModel(Answer source);
        List<TestAttemptAnswerViewModel> ToAttemptAnswerViewModels(List<Answer> source);
    }
}
