using System.Collections.Generic;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Params;

namespace TestCreator.Data.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Test GetTest(int id);
        Test GetTestWithInclude(int id);
        List<Test> GetTestsByParam(int number, TestsOrder order);
        List<Test> Search(string text, int number);
        Test CreateTest(Test test);
        Test UpdateTest(Test test);
        bool DeleteTest(int id);
    }
}
