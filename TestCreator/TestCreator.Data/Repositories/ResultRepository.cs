using System;
using System.Collections.Generic;
using System.Linq;
using TestCreator.Data.Context;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;

namespace TestCreator.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;

        public ResultRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Result GetResult(int id)
        {
            return _context.Results.FirstOrDefault(t => t.Id.Equals(id));
        }

        public List<Result> GetResults(int testId)
        {
            return _context.Results.Where(t => t.TestId == testId).ToList();
        }

        public Result CreateResult(Result result)
        {
            result.CreationDate = DateTime.Now;
            result.LastModificationDate = DateTime.Now;

            _context.Results.Add(result);
            _context.SaveChanges();

            return result;
        }

        public Result UpdateResult(Result result)
        {
            var resultToUpdate = _context.Results.FirstOrDefault(t => t.Id.Equals(result.Id));

            if (resultToUpdate == null)
            {
                return null;
            }

            resultToUpdate.TestId = result.TestId;
            resultToUpdate.Text = result.Text;
            resultToUpdate.Notes = result.Notes;
            resultToUpdate.MinValue = result.MinValue;
            resultToUpdate.MaxValue = result.MaxValue;

            resultToUpdate.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return result;
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
