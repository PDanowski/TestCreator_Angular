using Riok.Mapperly.Abstractions;
using TestCreator.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Mappers
{
    [Mapper]
    public partial class AppMapper : IAppMapper
    {
        public partial System.Collections.Generic.List<AnswerViewModel> ToViewModels(System.Collections.Generic.List<Answer> source);
        [MapperIgnoreSource(nameof(TestViewModel.UserCanEdit))]
        public partial Test ToModel(TestViewModel source);

        public partial AnswerViewModel ToViewModel(Answer source);
        public partial Answer ToModel(AnswerViewModel source);

        public partial System.Collections.Generic.List<QuestionViewModel> ToViewModels(System.Collections.Generic.List<Question> source);
        public partial QuestionViewModel ToViewModel(Question source);
        public partial Question ToModel(QuestionViewModel source);

        public partial System.Collections.Generic.List<ResultViewModel> ToViewModels(System.Collections.Generic.List<Result> source);
        public partial ResultViewModel ToViewModel(Result source);
        public partial Result ToModel(ResultViewModel source);

        public partial System.Collections.Generic.List<TestViewModel> ToViewModels(System.Collections.Generic.List<Test> source);
        public partial TestViewModel ToViewModel(Test source);

        public partial UserViewModel ToViewModel(ApplicationUser source);
        public partial ApplicationUser ToModel(UserViewModel source);

        public partial System.Collections.Generic.List<TestAttemptAnswerViewModel> ToAttemptAnswerViewModels(System.Collections.Generic.List<Answer> source);
        public partial TestAttemptAnswerViewModel ToAttemptAnswerViewModel(Answer source);
    }
}
