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
    public class TestController : BaseApiController
    {
        private readonly ITestRepository _repository;

        public TestController(ITestRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// GET: api/test/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var viewModel = _repository.GetTest(id);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Test with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// PUT: api/test/put
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPut]
        public IActionResult Put([FromBody]TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var createdViewModel = _repository.CreateTest(viewModel);
            return new JsonResult(createdViewModel, JsonSettings);
        }

        /// <summary>
        /// POST: api/test/post
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPost]
        public IActionResult Post([FromBody]TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var updatedViewModel = _repository.UpdateTest(viewModel);
            if (updatedViewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating test with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedViewModel, JsonSettings);
        }

        /// <summary>
        /// DELETE: api/test/delete
        /// </summary>
        /// <param name="id">Identifier of TestViewModel</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteTest(id))
            {
                return new NoContentResult();
            }
            return NotFound(new
            {
                Error = $"Error during deletion test with identifier {id}"
            });
        }


        /// <summary>
        /// GET api/test/latest
        /// </summary>
        /// <param name="num"></param>
        /// <returns>{num} latest TestViewModel</returns>
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latestTests = _repository.GetLatestTests(num);

            return new JsonResult(latestTests, JsonSettings);
        }

        /// <summary>
        /// GET: api/quiz/ByTitle
        /// </summary>
        /// <param name="num"></param>
        /// <returns>{num} TestViewModels order by title</returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var tests = _repository.GetTestsByTitle(num);

            return new JsonResult(tests, JsonSettings);
        }


        /// <summary>
        /// GET: api/quiz/Random
        /// </summary>
        /// <param name="num"></param>
        /// <returns>{num} random TestViewModels</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleTests = _repository.GetRandomTests(num);

            return new JsonResult(sampleTests?.OrderBy(t => Guid.NewGuid()), JsonSettings);
        }
    }
}
