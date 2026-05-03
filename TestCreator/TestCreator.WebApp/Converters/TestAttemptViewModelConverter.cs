using System.Collections.Generic;
using TestCreator.Data.Models;
using TestCreator.WebApp.Converters.Interfaces;
using TestCreator.WebApp.Mappers;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters
{
    public class TestAttemptViewModelConverter : ITestAttemptViewModelConverter
    {
        private readonly IAppMapper _mapper;

        public TestAttemptViewModelConverter(IAppMapper mapper)
        {
            _mapper = mapper;
        }

        public TestAttemptViewModel Convert(Test test)
        {
            if (test == null)
            {
                return null;
            }

            var viewModel = new TestAttemptViewModel
            {
                TestId = test.Id,
                Title = test.Title,
                TestAttemptEntries = new List<TestAttemptEntryViewModel>()
            };

            foreach (var question in test.Questions)
            {
                viewModel.TestAttemptEntries.Add(new TestAttemptEntryViewModel
                {
                    Question = _mapper.ToViewModel(question),
                    Answers = _mapper.ToAttemptAnswerViewModels(question.Answers)
                });
            }

            return viewModel;
        }
    }
}
