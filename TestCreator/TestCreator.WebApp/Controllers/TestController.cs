using System.Collections.Generic;
using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.Data.Repositories.Params;
using TestCreator.WebApp.Controllers.Attributes;
using TestCreator.WebApp.Converters.Interfaces;
using TestCreator.WebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreator.WebApp.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly ITestRepository _repository;

        private int _defaultQuerySize = 10;

        public TestController(ITestRepository testRepository)
        {
            this._repository = testRepository;
        }

        /// <summary>
        /// GET: api/test/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var test = _repository.GetTest(id);

            if (test == null)
            {
                return NotFound(new
                {
                    Error = $"Test with identifier {id} was not found"
                });
            }

            return new JsonResult(test.Adapt<TestViewModel>(), JsonSettings);
        }

        /// <summary>
        /// PUT: api/test/put
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPut]
        public IActionResult Put([FromBody] TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var updatedTest = _repository.UpdateTest(viewModel.Adapt<Test>());
            if (updatedTest == null)
            {
                return NotFound(new
                {
                    Error = $"Error during updating test with identifier {viewModel.Id}"
                });
            }
            return new JsonResult(updatedTest.Adapt<TestViewModel>(), JsonSettings);
        }

        /// <summary>
        /// POST: api/test/post
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            viewModel.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var createdTest = _repository.CreateTest(viewModel.Adapt<Test>());
            return new JsonResult(createdTest.Adapt<TestViewModel>(), JsonSettings);
        }

        /// <summary>
        /// DELETE: api/test/delete
        /// </summary>
        /// <param name="id">Identifier of TestViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
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
        /// GET api/test
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sorting">0 - random, 1 - latest, 2 - by title</param>
        /// <returns>{num} TestViewModel, sorted by param: {sorting}</returns>
        [HttpGet]
        public IActionResult GetBySorting([FromQuery] int sorting, [FromQuery] int? size = 10)
        {
            TestsOrder order;

            switch (sorting)
            {
                case 0:
                    order = TestsOrder.Random;
                    break;
                case 1:
                    order = TestsOrder.Latest;
                    break;
                case 2:
                    order = TestsOrder.ByTitle;
                    break;
                default:
                    return NotFound(new
                    {
                        Error = $"Sorting parameter has wrong value: {sorting}"
                    });
            }

            var tests = _repository.GetTestsByParam(size ?? _defaultQuerySize, order);

            return new JsonResult(tests.Adapt<List<TestViewModel>>(), JsonSettings);
        }

        /// <summary>
        /// GET: api/test/ByTitle
        /// </summary>
        /// <param name="text"></param>
        /// <param name="num"></param>
        /// <returns>{num} TestViewModels searched by title</returns>
        [HttpGet("Search/{num:int?}")]
        public IActionResult Search([FromQuery]string text, int num = 10)
        {
            var tests = _repository.Search(text, num);

            return new JsonResult(tests, JsonSettings);
        }


        /// <summary>
        /// GET: api/test
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>TestViewModels searched by keyword</returns>
        [HttpGet]
        [ExactQueryParam("keyword")]
        public IActionResult GetByKeyword([FromQuery] string keyword)
        {
            var tests = _repository.Search(keyword, _defaultQuerySize);

            return new JsonResult(tests.Adapt<List<TestViewModel>>(), JsonSettings);
        }
    }
}
