using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Abstract
{
    public interface ITestService
    {
        TestAttemptResultViewModel CalculateResult(TestAttemptViewModel viewModel);
    }
}
