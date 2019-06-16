using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreatorWebApp.Controllers
{
    public class ResultController : BaseApiController
    {
        private readonly IResultRepository _repository;

        public ResultController(IResultRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// GET: api/result/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ResultViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var viewModel = _repository.GetResult(id);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Result with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// PUT: api/result/put
        /// </summary>
        /// <param name="viewModel">ResultViewModel with data</param>
        [HttpPut]
        public IActionResult Put([FromBody]ResultViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var createdViewModel = _repository.CreateResult(viewModel);
            return new JsonResult(createdViewModel, JsonSettings);
        }

        /// <summary>
        /// POST: api/result/post
        /// </summary>
        /// <param name="viewModel">ResultViewModel with data</param>
        [HttpPost]
        public IActionResult Post([FromBody]ResultViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var updatedViewModel = _repository.UpdateResult(viewModel);
            if (updatedViewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating result with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedViewModel, JsonSettings);
        }

        /// <summary>
        /// DELETE: api/result/delete
        /// </summary>
        /// <param name="id">Identifier of ResultViewModel</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteResult(id))
            {
                return new NoContentResult();
            }
            return NotFound(new
            {
                Error = $"Error during deletion question with identifier {id}"
            });
        }

        /// <summary>
        /// GET: api/result/all
        /// </summary>
        /// <param name="testId"></param>
        /// <returns>All ResultViewModel for given {quizId}</returns>
        [HttpGet("All/{testId}")]
        public IActionResult All(int testId)
        {
            var viewModels = _repository.GetResults(testId);

            if (viewModels == null)
            {
                return NotFound(new
                {
                    Error = $"Results for test with identifier {testId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }


    }
}
