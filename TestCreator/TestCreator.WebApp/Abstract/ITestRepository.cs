using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface ITestRepository
    {
        TestViewModel GetTest(int id);
        TestAttemptViewModel StartTest(int id);
        List<TestViewModel> GetLatestTests(int number);
        List<TestViewModel> GetTestsByTitle(int number);
        List<TestViewModel> GetRandomTests(int number);
        List<TestViewModel> Search(string text, int number);
        TestViewModel CreateTest(TestViewModel viewModel);
        TestViewModel UpdateTest(TestViewModel viewModel);
        bool DeleteTest(int id);
    }
}
