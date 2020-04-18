using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.ViewModels
{
    public class TestAttemptResultViewModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
        public int MaximalPossibleScore { get; set; }

    }
}
