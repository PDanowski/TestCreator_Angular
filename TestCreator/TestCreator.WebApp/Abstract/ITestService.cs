using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface ITestService
    {
        TestAttemptResultViewModel CalculateResult(TestAttemptViewModel viewModel);
    }
}
