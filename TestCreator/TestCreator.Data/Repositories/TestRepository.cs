using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Context;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.Data.Repositories.Params;

namespace TestCreator.Data.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Test GetTest(int id)
        {
            return _context.Tests.FirstOrDefault(t => t.Id.Equals(id));
        }

        public Test GetTestWithInclude(int id)
        {
            return _context.Tests.Where(t => t.Id.Equals(id))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();
        }

        public List<Test> GetTestsByParam(int number, TestsOrder order)
        {
            IQueryable<Test> tests = null;

            switch (order)
            {
                case TestsOrder.ByTitle:
                    tests = _context.Tests.OrderBy(t => t.Title).AsNoTracking();
                    break;
                case TestsOrder.Latest:
                    tests = _context.Tests.OrderByDescending(t => t.CreationDate).AsNoTracking();
                    break;
                case TestsOrder.Random:
                    tests = _context.Tests.OrderBy(t => Guid.NewGuid()).AsNoTracking();
                    break;
            }

            return tests!.ToList();
        }

        public List<Test> Search(string text, int number)
        {
            return _context.Tests.Where(t => t.Title.Contains(text))
                .Take(number)
                .ToList();
        }

        public Test CreateTest(Test test)
        {
            test.CreationDate = DateTime.Now;
            test.LastModificationDate = DateTime.Now;
            test.UserId = _context.Users.FirstOrDefault(u => u.UserName.Equals("Admin"))?.Id;

            _context.Tests.Add(test);
            _context.SaveChanges();

            return test;
        }

        public Test UpdateTest(Test test)
        {
            var testToUpdate = _context.Tests.FirstOrDefault(t => t.Id.Equals(test.Id));

            if (testToUpdate == null)
            {
                return null;
            }

            testToUpdate.Title = test.Title;
            testToUpdate.Description = test.Description;
            testToUpdate.Text = test.Text;
            testToUpdate.Notes = test.Notes;

            test.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return testToUpdate;
        }

        public bool DeleteTest(int id)
        {
            var test = _context.Tests.FirstOrDefault(t => t.Id.Equals(id));

            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);
            return _context.SaveChanges() > 0;
        }
    }
}
