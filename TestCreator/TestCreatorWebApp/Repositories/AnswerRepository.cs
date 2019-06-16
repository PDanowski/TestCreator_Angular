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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public AnswerViewModel GetAnswer(int id)
        {
            var answer = _context.Answers.FirstOrDefault(t => t.Id.Equals(id));
            return answer.Adapt<AnswerViewModel>();
        }

        public List<AnswerViewModel> GetAnswers(int questionId)
        {
            //var answers = _context.Answers.Where(t => t.QuestionId == questionId).ToList();
            var answers1 = _context.Answers.ToList();
            var answers2 = answers1.Where(t => t.QuestionId == questionId);

            return answers2.Adapt<List<AnswerViewModel>>();
        }

        public AnswerViewModel CreateAnswer(AnswerViewModel viewModel)
        {
            var answer = viewModel.Adapt<Answer>();

            answer.CreationDate = DateTime.Now;
            answer.LastModificationDate = DateTime.Now;

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return answer.Adapt<AnswerViewModel>();
        }

        public AnswerViewModel UpdateAnswer(AnswerViewModel viewModel)
        {
            var answer = _context.Answers.FirstOrDefault(t => t.Id.Equals(viewModel.Id));

            if (answer == null)
            {
                return null;
            }

            answer.QuestionId = viewModel.QuestionId;
            answer.Text = viewModel.Text;
            answer.Notes = viewModel.Notes;
            answer.Value = viewModel.Value;

            answer.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return answer.Adapt<AnswerViewModel>();
        }

        public bool DeleteAnswer(int id)
        {
            var answer = _context.Answers.FirstOrDefault(t => t.Id.Equals(id));

            if (answer == null)
            {
                return false;
            }

            _context.Answers.Remove(answer);
            return _context.SaveChanges() > 0;
        }
    }
}
