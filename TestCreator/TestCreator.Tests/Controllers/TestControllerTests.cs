using System.Security.Claims;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Broadcast;
using TestCreator.WebApp.Broadcast.Interfaces;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestControllerTests
    {
        private Mock<ITestRepository> _mockRepo;
        private Mock<IHubContext<TestsHub, ITestsHubClient>> _mockHub;

        private TestController _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockRepo = new Mock<ITestRepository>();
            _mockHub = new Mock<IHubContext<TestsHub, ITestsHubClient>>();
            _sut = new TestController(_mockRepo.Object, _mockHub.Object);
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

            _mockRepo.Setup(x => x.GetTest(testId)).Returns(Task.FromResult(test));

            var result = _sut.Get(testId).Result as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Get_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var testId = 2;
            _mockRepo.Setup(x => x.GetTest(testId)).Returns(Task.FromResult((Test)null));

            var result = _sut.Get(testId).Result;

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

            _mockRepo.Setup(x => x.CreateTest(It.Is<Test>(t => t.Id == testId))).Returns(Task.FromResult(test));
            _mockHub.Setup(x => x.Clients.All.TestCreated()).Returns(Task.CompletedTask);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "name1")
            }, "mock"));

            var controller = new TestController(_mockRepo.Object, _mockHub.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var result = controller.Post(test.Adapt<TestViewModel>()).Result as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Post_WhenInvalidViewModelGiven_ShouldReturnStatusCode500()
        {
            var result = _sut.Post(null).Result as StatusCodeResult;

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

            _mockRepo.Setup(x => x.UpdateTest(It.Is<Test>(t => t.Id == testId))).Returns(Task.FromResult(test));

            var result = _sut.Put(test.Adapt<TestViewModel>()).Result as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), test.Id);
        }

        [Test]
        public void Put_WhenInvalidViewModelGiven_ShouldReturnsStatusCode500()
        {
            var result = _sut.Put(null).Result as StatusCodeResult;

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

            var result = _sut.Put(test.Adapt<TestViewModel>()).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var testId = 1;

            _mockRepo.Setup(x => x.DeleteTest(testId)).Returns(Task.FromResult(true));
            _mockHub.Setup(x => x.Clients.All.TestRemoved(It.IsAny<int>())).Returns(Task.CompletedTask);

            var result = _sut.Delete(testId).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var testId = 2;
            _mockRepo.Setup(x => x.DeleteTest(testId)).Returns(Task.FromResult(false));

            var result = _sut.Delete(testId).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
