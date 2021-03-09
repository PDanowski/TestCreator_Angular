using System;
using System.Collections.Generic;
using System.Linq;
using TestCreator.Data.Context;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;

namespace TestCreator.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Question GetQuestion(int id)
        {
            return _context.Questions.FirstOrDefault(t => t.Id.Equals(id));
        }

        public List<Question> GetQuestions(int testId)
        {
            return _context.Questions.Where(t => t.TestId == testId).ToList();
        }

        public Question CreateQuestion(Question question)
        {
            question.CreationDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question;
        }

        public Question UpdateQuestion(Question question)
        {
            var questionToUpdate = _context.Questions.FirstOrDefault(t => t.Id.Equals(question.Id));

            if (questionToUpdate == null)
            {
                return null;
            }

            questionToUpdate.TestId = question.TestId;
            questionToUpdate.Text = question.Text;
            questionToUpdate.Notes = question.Notes;

            questionToUpdate.LastModificationDate = DateTime.Now;

            _context.SaveChanges();

            return questionToUpdate;
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
