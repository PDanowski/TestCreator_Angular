using System.Collections.Generic;
using TestCreator.Data.Models;

namespace TestCreator.Data.Repositories.Interfaces
{
    public interface IResultRepository
    {
        Result GetResult(int id);
        List<Result> GetResults(int testId);
        Result CreateResult(Result result);
        Result UpdateResult(Result result);
        bool DeleteResult(int id);
    }
}
