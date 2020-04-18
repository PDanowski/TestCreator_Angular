using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.Data;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }


        public QuestionViewModel GetQuestion(int id)
        {
            var question = _context.Questions.FirstOrDefault(t => t.Id.Equals(id));
            return question.Adapt<QuestionViewModel>();
        }

        public List<QuestionViewModel> GetQuestions(int testId)
        {
            var questions = _context.Questions.Where(t => t.TestId == testId).ToList();
            return questions.Adapt<List<QuestionViewModel>>();
        }

        public QuestionViewModel CreateQuestion(QuestionViewModel viewModel)
        {
            var question = viewModel.Adapt<Question>();

            question.CreationDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question.Adapt<QuestionViewModel>();
        }

        public QuestionViewModel UpdateQuestion(QuestionViewModel viewModel)
        {
            var question = _context.Questions.FirstOrDefault(t => t.Id.Equals(viewModel.Id));

            if (question == null)
            {
                return null;
            }

            question.TestId = viewModel.TestId;
            question.Text = viewModel.Text;
            question.Notes = viewModel.Notes;

            question.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return question.Adapt<QuestionViewModel>();
        }

        public bool DeleteQuestion(int id)
        {
            var question = _context.Questions.FirstOrDefault(t => t.Id.Equals(id));

            if (question == null)
            {
                return false;
            }

            _context.Questions.Remove(question);
            return _context.SaveChanges() > 0;
        }
    }
}
