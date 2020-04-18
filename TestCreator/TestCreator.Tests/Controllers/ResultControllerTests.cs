using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ResultControllerTests
    {
        [Test]
        public void Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModel = new ResultViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.GetResult(It.IsAny<int>())).Returns(viewModel);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.GetResult(It.IsAny<int>())).Returns<Result>(null);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void All_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModels = new List<ResultViewModel>
            {
                new ResultViewModel
                {
                    Id = 1,
                    Text = "Text1"
                },
                new ResultViewModel
                {
                    Id = 2,
                    Text = "Text2"
                }
            };

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.GetResults(It.IsAny<int>())).Returns(viewModels);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.All(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Text, viewModels.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Id, viewModels.First().Id);
        }

        [Test]
        public void All_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.GetResults(It.IsAny<int>())).Returns<List<ResultViewModel>>(null);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.All(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new ResultViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.CreateResult(It.IsAny<ResultViewModel>())).Returns(viewModel);


            var controller = new ResultController(mockRepo.Object);

            var result = controller.Put(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IResultRepository>();

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new ResultViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.UpdateResult(It.IsAny<ResultViewModel>())).Returns(viewModel);

            var controller =
                new ResultController(mockRepo.Object);

            var result = controller.Post(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IResultRepository>();

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var viewModel = new ResultViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.UpdateResult(It.IsAny<ResultViewModel>())).Returns<ResultViewModel>(null);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Post(viewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            int id = 1;

            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.DeleteResult(It.IsAny<int>())).Returns(true);

            var controller =
                new ResultController(mockRepo.Object);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var mockRepo = new Mock<IResultRepository>();
            mockRepo.Setup(x => x.DeleteResult(1)).Returns(false);

            var controller = new ResultController(mockRepo.Object);

            var result = controller.Delete(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
