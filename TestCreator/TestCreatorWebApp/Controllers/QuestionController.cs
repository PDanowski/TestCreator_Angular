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
    public class QuestionController : Controller
    {
        /// <summary>
        /// GET: api/question/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>QuestionViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented yet");
        }

        /// <summary>
        /// PUT: api/question/put
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPut]
        public IActionResult Put(QuestionViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/question/post
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPost]
        public IActionResult Post(QuestionViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/question/delete
        /// </summary>
        /// <param name="id">Identifier of QuestionViewModel</param>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GET: api/question/all
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns>All QuestionViewModel for given {quizId}</returns>
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>
            {
                new QuestionViewModel
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
                sampleQuestions.Add(new QuestionViewModel
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
