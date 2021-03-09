using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestControllerTests
    {
        private Mock<ITestRepository> _mockRepo;

        private TestController _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockRepo = new Mock<ITestRepository>();
            _sut = new TestController(_mockRepo.Object);
        }

        [Test]
        public void Get_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            var testId = 1;
            var test = new Test
            {
                Id = 1,
                Title = "title1"
            };

            _mockRepo.Setup(x => x.GetTest(testId)).Returns(test);

            var result = _sut.Get(testId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Get_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var testId = 2;
            _mockRepo.Setup(x => x.GetTest(testId)).Returns<Test>(null);

            var result = _sut.Get(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var testId = 1;
            var test = new Test
            {
                Id = testId,
                Title = "title1"
            };

            _mockRepo.Setup(x => x.CreateTest(It.Is<Test>(t => t.Id == testId))).Returns(test);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "name1")
            }, "mock"));

            var controller = new TestController(_mockRepo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var result = controller.Post(test.Adapt<TestViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Post_WhenInvalidViewModelGiven_ShouldReturnStatusCode500()
        {
            var result = _sut.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_WhenCorrectViewModelGiven_ShouldReturnsJsonViewModel()
        {
            var testId = 1;
            var test = new Test
            {
                Id = 1,
                Title = "title1"
            };

            _mockRepo.Setup(x => x.UpdateTest(It.Is<Test>(t => t.Id == testId))).Returns(test);

            var result = _sut.Put(test.Adapt<TestViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Put_WhenInvalidViewModelGiven_ShouldReturnsStatusCode500()
        {
            var result = _sut.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_WhenCorrectTestAttemptViewModelErrorDuringProcessing_ShouldReturnsNotFound()
        {
            var test = new Test
            {
                Id = 1,
                Title = "title1"
            };

            _mockRepo.Setup(x => x.UpdateTest(test)).Returns<TestViewModel>(null);

            var result = _sut.Put(test.Adapt<TestViewModel>());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var testId = 1;

            _mockRepo.Setup(x => x.DeleteTest(testId)).Returns(true);

            var result = _sut.Delete(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var testId = 2;
            _mockRepo.Setup(x => x.DeleteTest(testId)).Returns(false);

            var result = _sut.Delete(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
