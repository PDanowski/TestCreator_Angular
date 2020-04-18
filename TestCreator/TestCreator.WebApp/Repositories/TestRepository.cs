using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.Data;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public TestViewModel GetTest(int id)
        {
            var test = _context.Tests.FirstOrDefault(t => t.Id.Equals(id));
            return test.Adapt<TestViewModel>();
        }

        public TestAttemptViewModel StartTest(int id)
        {
            var test = _context.Tests.Where(t => t.Id.Equals(id))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            if (test == null)
            {
                return null;
            }

            var viewModel = new TestAttemptViewModel
            {
                TestId = test.Id,
                Title = test.Title,
                TestAttemptEntries = new List<TestAttemptEntryViewModel>()
            };

            foreach (var question in test.Questions)
            {
                viewModel.TestAttemptEntries.Add(new TestAttemptEntryViewModel
                {
                    Question = question.Adapt<QuestionViewModel>(),
                    Answers = question.Answers.Adapt<List<TestAttemptAnswerViewModel>>()
                });
            }

            return viewModel;
        }



        public List<TestViewModel> GetLatestTests(int number)
        {
            var latest = _context.Tests.OrderByDescending(t => t.CreationDate)
                .Take(number)
                .ToList();
            return latest.Adapt<List<TestViewModel>>();
        }

        public List<TestViewModel> GetTestsByTitle(int number)
        {
            var latest = _context.Tests.OrderBy(t => t.Title)
                .Take(number)
                .ToList();
            return latest.Adapt<List<TestViewModel>>();
        }

        public List<TestViewModel> GetRandomTests(int number)
        {
            var random = _context.Tests.OrderBy(t => Guid.NewGuid())
                .Take(number)
                .ToList();
            return random.Adapt<List<TestViewModel>>();
        }

        public List<TestViewModel> Search(string text, int number)
        {
            var random = _context.Tests.Where(t => t.Title.Contains(text))
                .Take(number)
                .ToList();
            return random.Adapt<List<TestViewModel>>();
        }

        public TestViewModel CreateTest(TestViewModel viewModel)
        {
            var test = viewModel.Adapt<Test>();

            test.CreationDate = DateTime.Now;
            test.LastModificationDate = DateTime.Now;
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

            test.LastModificationDate = DateTime.Now;

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
            return _context.SaveChanges() > 0;
        }
    }
}
