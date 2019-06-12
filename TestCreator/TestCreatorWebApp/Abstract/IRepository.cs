using System.Collections.Generic;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface IRepository
    {
        TestViewModel GetTest(int id);
        List<TestViewModel> GetLatestTests(int number);
        List<TestViewModel> GetTestsByTitle(int number);
        List<TestViewModel> GetRandomTests(int number);
        TestViewModel CreateTest(TestViewModel viewModel);
        TestViewModel UpdateTest(TestViewModel viewModel);
        bool DeleteTest(int id);
    }
}
