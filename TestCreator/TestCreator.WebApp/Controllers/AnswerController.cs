using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreatorWebApp.Controllers
{
    public class AnswerController : BaseApiController
    {
        private readonly IAnswerRepository _repository;

        public AnswerController(IAnswerRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// GET: api/answer/all
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>All AnswerViewModel for given {questionId}</returns>
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var viewModels = _repository.GetAnswers(questionId);

            if (viewModels == null)
            {
                return NotFound(new
                {
                    Error = $"Answers for question with identifier {questionId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }

        /// <summary>
        /// GET: api/answer/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AnswerViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var viewModel = _repository.GetAnswer(id);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Answer with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
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

            var createdViewModel = _repository.CreateAnswer(viewModel);
            return new JsonResult(createdViewModel, JsonSettings);
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

            var updatedViewModel = _repository.UpdateAnswer(viewModel);
            if (updatedViewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating answer with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedViewModel, JsonSettings);
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
