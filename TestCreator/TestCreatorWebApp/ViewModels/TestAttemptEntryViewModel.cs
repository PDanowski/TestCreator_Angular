using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.ViewModels
{
    public class TestAttemptEntryViewModel
    {
        public QuestionViewModel Question { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public int? SelectedAnswerId { get; set; }
    }
}
