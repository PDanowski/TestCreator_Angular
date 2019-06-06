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
    public class AnswerController : Controller
    {
        /// <summary>
        /// GET: api/answer/all
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>All AnswerViewModel for given {questionId}</returns>
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleQuestions = new List<AnswerViewModel>
            {
                new AnswerViewModel
                {
                    Id = 1,
                    QuestionId = questionId,
                    Text = "Test answer",
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                }

            };

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new AnswerViewModel
                {
                    Id = i,
                    Text = "Sample answer" + i,
                    QuestionId = questionId,
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

        /// <summary>
        /// GET: api/answer/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AnswerViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented yet");
        }

        /// <summary>
        /// PUT: api/answer/put
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPut]
        public IActionResult Put(AnswerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/answer/post
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPost]
        public IActionResult Post(AnswerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST: api/answer/delete
        /// </summary>
        /// <param name="id">Identifier of AnswerViewModel</param>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
