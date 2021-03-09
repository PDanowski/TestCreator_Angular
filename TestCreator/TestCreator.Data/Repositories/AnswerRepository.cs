using System;
using System.Collections.Generic;
using System.Linq;
using TestCreator.Data.Context;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;

namespace TestCreator.Data.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Answer GetAnswer(int id)
        {
            return _context.Answers.FirstOrDefault(t => t.Id.Equals(id));
        }

        public List<Answer> GetAnswers(int questionId)
        {
            var answers1 = _context.Answers.ToList();
            return answers1.Where(t => t.QuestionId == questionId).ToList();
        }

        public Answer CreateAnswer(Answer answer)
        {
            answer.CreationDate = DateTime.Now;
            answer.LastModificationDate = DateTime.Now;

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return answer;
        }

        public Answer UpdateAnswer(Answer answer)
        {
            var answerToUpdate = _context.Answers.FirstOrDefault(t => t.Id.Equals(answer.Id));

            if (answerToUpdate == null)
            {
                return null;
            }

            answerToUpdate.QuestionId = answer.QuestionId;
            answerToUpdate.Text = answer.Text;
            answerToUpdate.Notes = answer.Notes;
            answerToUpdate.Value = answer.Value;

            answerToUpdate.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return answerToUpdate;
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
