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
    public class TestController : Controller
    {
        //GET api/test/latest
        [HttpGet("Latest/{num?}")]
        public IActionResult Latest(int num = 10)
        {
            var sampleTests = new List<TestViewModel>
            {
                new TestViewModel
                {
                    Id = 1,
                    Title = "Sample test",
                    Description = "Sample desciption",
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                }
            };


            for (int i = 2; i <= num; i++)
            {
                sampleTests.Add(new TestViewModel
                {
                    Id = i,
                    Title = "Sample test" + i,
                    Description = "Sample desciption" + i,
                    CreationDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                });
            }

            return new JsonResult(sampleTests, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        //GET: api/quiz/ByTitle
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleTests = ((JsonResult) Latest(num)).Value as List<TestViewModel>;

            return new JsonResult(sampleTests.OrderBy(t => t.Title),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }

        //GET: api/quiz/Random
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleTests = ((JsonResult)Latest(num)).Value as List<TestViewModel>;

            return new JsonResult(sampleTests.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
