using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Data;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Implementations
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public TestViewModel GetTest(int id)
        {
            var test = _context.Tests.FirstOrDefault(t => t.Id.Equals(id));
            return test.Adapt<TestViewModel>();
        }

        public List<TestViewModel> GetLatestTests(int number)
        {
            var latest = _context.Tests.OrderByDescending(t => t.CreationDate)
                .Take(number)
                .ToArray();
            return latest.Adapt<List<TestViewModel>>();
        }

        public List<TestViewModel> GetTestsByTitle(int number)
        {
            var latest = _context.Tests.OrderBy(t => t.Title)
                .Take(number)
                .ToArray();
            return latest.Adapt<List<TestViewModel>>();
        }

        public List<TestViewModel> GetRandomTests(int number)
        {
            var random = _context.Tests.OrderBy(t => Guid.NewGuid())
                .Take(number)
                .ToArray();
            return random.Adapt<List<TestViewModel>>();
        }

        public TestViewModel CreateTest(TestViewModel viewModel)
        {
            var test = viewModel.Adapt<Test>();

            test.CreationDate = DateTime.Now;
            test.UserId = _context.Users.FirstOrDefault(u => u.UserName.Equals("Admin"))?.Id;

            _context.Tests.Add(test);
            _context.SaveChanges();

            return test.Adapt<TestViewModel>();
        }

        public TestViewModel UpdateTest(TestViewModel viewModel)
        {
            var test = _context.Tests.FirstOrDefault(t => t.Id.Equals(viewModel.Id));

            if (test == null)
            {
                return null;
            }

            test.Title = viewModel.Title;
            test.Description = viewModel.Description;
            test.Text = viewModel.Text;
            test.Notes = viewModel.Notes;

            test.LastModificationDate =  DateTime.Now;

            _context.SaveChanges();

            return test.Adapt<TestViewModel>();
        }

        public bool DeleteTest(int id)
        {
            var test = _context.Tests.FirstOrDefault(t => t.Id.Equals(id));

            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);
            return _context .SaveChanges() > 0;
        }
    }
}
