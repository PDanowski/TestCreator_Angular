using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreator.WebApp.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionRepository _repository;

        public QuestionController(IQuestionRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// GET: api/question/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>QuestionViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var viewModel = _repository.GetQuestion(id);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Question with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// PUT: api/question/put
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]QuestionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var updatedViewModel = _repository.UpdateQuestion(viewModel);
            if (updatedViewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating question with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedViewModel, JsonSettings);
        }

        /// <summary>
        /// POST: api/question/post
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]QuestionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var createdViewModel = _repository.CreateQuestion(viewModel);
            return new JsonResult(createdViewModel, JsonSettings);
        }

        /// <summary>
        /// DELETE: api/question/delete
        /// </summary>
        /// <param name="id">Identifier of QuestionViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteQuestion(id))
            {
                return new NoContentResult();
            }
            return NotFound(new
            {
                Error = $"Error during deletion question with identifier {id}"
            });
        }

        /// <summary>
        /// GET: api/question/all
        /// </summary>
        /// <param name="testId"></param>
        /// <returns>All QuestionViewModel for given {quizId}</returns>
        [HttpGet("All/{testId}")]
        public IActionResult All(int testId)
        {
            var viewModels = _repository.GetQuestions(testId);

            if (viewModels == null)
            {
                return NotFound(new
                {
                    Error = $"Questions for test with identifier {testId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }

    }
}
