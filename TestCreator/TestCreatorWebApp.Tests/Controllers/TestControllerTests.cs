using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Controllers;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.Tests.Helpers;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Tests.Controllers
{
    [TestFixture]
    public class TestControllerTests
    {
        [Test]
        public void Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModel = new TestViewModel
            {
                Id = 1,
                Title = "title1"
            };

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.GetTest(It.IsAny<int>())).Returns(viewModel);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        }

        [Test]
        public void Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.GetTest(It.IsAny<int>())).Returns<Test>(null);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Start_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModel = new TestAttemptViewModel()
            {
                TestId = 1,
                Title = "title1",
                TestAttemptEntries = new List<TestAttemptEntryViewModel>
                {
                    new TestAttemptEntryViewModel()
                }
            };

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.StartTest(It.IsAny<int>())).Returns(viewModel);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Start(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), viewModel.TestId);
            Assert.IsNotNull(result.GetValueFromJsonResult<List<TestAttemptEntryViewModel>>("TestAttemptEntries"));
        }

        [Test]
        public void Start_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.StartTest(It.IsAny<int>())).Returns<Test>(null);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Result_CorrectTestAttemptViewModel_ReturnsJsonViewModel()
        {
            var viewModel = new TestAttemptViewModel()
            {
                TestId = 1,
                Title = "title1",
                TestAttemptEntries = new List<TestAttemptEntryViewModel>
                {
                    new TestAttemptEntryViewModel()
                }
            };

            var returnedViewModel = new TestAttemptResultViewModel()
            {
                TestId = 1,
                Title = "title1",
                Score = 10,
                MaximalPossibleScore = 15
            };

            var mockRepo = new Mock<ITestService>();
            mockRepo.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns(returnedViewModel);

            var controller = new TestController(null, mockRepo.Object);

            var result = controller.Result(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), returnedViewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), returnedViewModel.TestId);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Score"), returnedViewModel.Score);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("MaximalPossibleScore"), returnedViewModel.MaximalPossibleScore);
        }

        [Test]
        public void Result_NullTestAttemptViewModel_ReturnsNotFound()
        {
            var controller = new TestController(null, null);

            var result = controller.Result(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Result_CorrectTestAttemptViewModelErrorDuringCalculating_ReturnsNotFound()
        {
            var viewModel = new TestAttemptViewModel()
            {
                TestId = 1,
                Title = "title1",
                TestAttemptEntries = new List<TestAttemptEntryViewModel>
                {
                    new TestAttemptEntryViewModel()
                }
            };

            var mockRepo = new Mock<ITestService>();
            mockRepo.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns<TestAttemptResultViewModel>(null);

            var controller = new TestController(null, mockRepo.Object);

            var result = controller.Result(viewModel) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }


        [Test]
        public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new TestViewModel
            {
                Id = 1,
                Title = "title1"
            };

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.CreateTest(It.IsAny<TestViewModel>())).Returns(viewModel);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "name1")
            }, "mock"));

            var controller = new TestController(mockRepo.Object, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var result = controller.Put(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        }

        [Test]
        public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<ITestRepository>();

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new TestViewModel
            {
                Id = 1,
                Title = "title1"
            };

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.UpdateTest(It.IsAny<TestViewModel>())).Returns(viewModel);

            var controller =
                new TestController(mockRepo.Object, null);

            var result = controller.Post(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        }

        [Test]
        public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<ITestRepository>();

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var viewModel = new TestViewModel
            {
                Id = 1,
                Title = "title1"
            };

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.UpdateTest(It.IsAny<TestViewModel>())).Returns<TestViewModel>(null);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Post(viewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            int id = 1;

            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.DeleteTest(It.IsAny<int>())).Returns(true);

            var controller =
                new TestController(mockRepo.Object, null);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var mockRepo = new Mock<ITestRepository>();
            mockRepo.Setup(x => x.DeleteTest(1)).Returns(false);

            var controller = new TestController(mockRepo.Object, null);

            var result = controller.Delete(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
