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
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private IRepository _repository;

        public TestController(IRepository repository)
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

            return new JsonResult(viewModel, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// PUT: api/test/put
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPut]
        public IActionResult Put(TestViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/test/post
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPost]
        public IActionResult Post(TestViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/test/delete
        /// </summary>
        /// <param name="id">Identifier of TestViewModel</param>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
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

            return new JsonResult(latestTests, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
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

            return new JsonResult(tests,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
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

            return new JsonResult(sampleTests?.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
