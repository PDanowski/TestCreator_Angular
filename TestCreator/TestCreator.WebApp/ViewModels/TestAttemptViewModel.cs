using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.ViewModels
{
    public class TestAttemptViewModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }

        public List<TestAttemptEntryViewModel> TestAttemptEntries { get; set; }
    }
}
