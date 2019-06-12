using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
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
