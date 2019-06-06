using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestCreatorWebApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCreatorWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        /// <summary>
        /// GET: api/result/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ResultViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented yet");
        }

        /// <summary>
        /// PUT: api/result/put
        /// </summary>
        /// <param name="viewModel">ResultViewModel with data</param>
        [HttpPut]
        public IActionResult Put(ResultViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/result/post
        /// </summary>
        /// <param name="viewModel">ResultViewModel with data</param>
        [HttpPost]
        public IActionResult Post(ResultViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/result/delete
        /// </summary>
        /// <param name="id">Identifier of ResultViewModel</param>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GET: api/result/all
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns>All ResultViewModel for given {quizId}</returns>
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<ResultViewModel>
            {
                new ResultViewModel
                {
                    Id = 1,
                    QuizId = quizId,
                    Text = "Test question",
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                }

            };

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new ResultViewModel
                {
                    Id = i,
                    Text = "Sample question" + i,
                    QuizId = quizId,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                });
            }

            return new JsonResult(sampleQuestions,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }


    }
}
