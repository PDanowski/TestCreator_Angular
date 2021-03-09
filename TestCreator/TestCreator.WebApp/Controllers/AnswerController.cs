using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.WebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreator.WebApp.Controllers
{
    public class AnswerController : BaseApiController
    {
        private readonly IAnswerRepository _repository;

        public AnswerController(IAnswerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// GET: api/answer/
        /// </summary>
        /// <returns>All AnswerViewModel for given {questionId}</returns>
        [HttpGet]
        public IActionResult GetByQuestionId([Required][FromQuery(Name = "questionId")] int questionId)
        {
            var answers = _repository.GetAnswers(questionId);

            if (answers == null)
            {
                return NotFound(new
                {
                    Error = $"Answers for question with identifier {questionId} were not found"
                });
            }

            return new JsonResult(answers.Adapt<List<AnswerViewModel>>(), JsonSettings);
        }

        /// <summary>
        /// GET: api/answer/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AnswerViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = _repository.GetAnswer(id);

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = $"Answer with identifier {id} was not found"
                });
            }

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// PUT: api/answer/put
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]AnswerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var updatedAnswer = _repository.UpdateAnswer(viewModel.Adapt<Answer>());
            if (updatedAnswer == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating answer with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedAnswer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// POST: api/answer/post
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]AnswerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var createdAnswer = _repository.CreateAnswer(viewModel.Adapt<Answer>());
            return new JsonResult(createdAnswer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// DELETE: api/answer/delete
        /// </summary>
        /// <param name="id">Identifier of AnswerViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteAnswer(id))
            {
                return new NoContentResult();
            }
            return NotFound(new
            {
                Error = $"Error during deletion answer with identifier {id}"
            });
        }
    }
}
