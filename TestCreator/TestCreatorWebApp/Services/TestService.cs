using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Services
{
    public class TestService : ITestService
    {
        public TestAttemptResultViewModel CalculateResult(TestAttemptViewModel viewModel)
        {
            var maxScore = viewModel.TestAttemptEntries.Sum(e => e.Answers.Where(a => a.Value >= 0).Sum(a => a.Value));
            var score = viewModel.TestAttemptEntries.Where(e => !e.IsMultitipleChoise)
                            .Sum(e => e.Answers.Where(a => a.Checked).Sum(a => a.Value)) +
                        viewModel.TestAttemptEntries.Where(e => e.IsMultitipleChoise && CheckIfAllAnswersCheckedAreCorrect(e.Answers))
                            .Sum(e => e.Answers.Where(a => a.Checked).Sum(a => a.Value));

            return new TestAttemptResultViewModel
            {
                MaximalPossibleScore = maxScore,
                Score = score,
                TestId = viewModel.TestId,
                Title = viewModel.Title
            };
        }

        private bool CheckIfAllAnswersCheckedAreCorrect(List<TestAttemptAnswerViewModel> answers)
        {
            return answers.Count(a => a.Checked && a.Value >= 0) == answers.Count(a => a.Checked);
        }
    }
}
