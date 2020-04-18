using System.Collections.Generic;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Abstract
{
    public interface IResultRepository
    {
        ResultViewModel GetResult(int id);
        List<ResultViewModel> GetResults(int testId);
        ResultViewModel CreateResult(ResultViewModel viewModel);
        ResultViewModel UpdateResult(ResultViewModel viewModel);
        bool DeleteResult(int id);
    }
}
