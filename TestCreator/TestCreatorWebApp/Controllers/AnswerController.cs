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
        // GET: api/answer/all
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

    }
}
