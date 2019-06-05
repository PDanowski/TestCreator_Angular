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
        // GET: api/result/all
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
