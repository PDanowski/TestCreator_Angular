using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.ViewModels
{
    public class TestAttemptEntryViewModel
    {
        public QuestionViewModel Question { get; set; }
        public List<TestAttemptAnswerViewModel> Answers { get; set; }
        public bool IsMultitipleChoise => Answers.Count(a => a.Value > 0) > 1;
    }
}
