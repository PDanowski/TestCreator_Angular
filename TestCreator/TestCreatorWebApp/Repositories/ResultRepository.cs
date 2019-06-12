using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Data;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;

        public ResultRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public ResultViewModel GetResult(int id)
        {
            var result = _context.Results.FirstOrDefault(t => t.Id.Equals(id));
            return result.Adapt<ResultViewModel>();
        }

        public List<ResultViewModel> GetResults(int testId)
        {
            var results = _context.Results.Where(t => t.TestId == testId).ToList();
            return results.Adapt<List<ResultViewModel>>();
        }

        public ResultViewModel CreateResult(ResultViewModel viewModel)
        {
            var result = viewModel.Adapt<Result>();

            result.CreationDate = DateTime.Now;
            result.LastModificationDate = DateTime.Now;

            _context.Results.Add(result);
            _context.SaveChanges();

            return result.Adapt<ResultViewModel>();
        }

        public ResultViewModel UpdateResult(ResultViewModel viewModel)
        {
            var result = _context.Results.FirstOrDefault(t => t.Id.Equals(viewModel.Id));

            if (result == null)
            {
                return null;
            }

            result.TestId = viewModel.TestId;
            result.Text = viewModel.Text;
            result.Notes = viewModel.Notes;
            result.MinValue = viewModel.MinValue;
            result.MaxValue = viewModel.MaxValue;

            result.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return result.Adapt<ResultViewModel>();
        }

        public bool DeleteResult(int id)
        {
            var result = _context.Results.FirstOrDefault(t => t.Id.Equals(id));

            if (result == null)
            {
                return false;
            }

            _context.Results.Remove(result);
            return _context.SaveChanges() > 0;
        }
    }
}
