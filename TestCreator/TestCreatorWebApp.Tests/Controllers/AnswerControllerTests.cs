using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Controllers;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.Tests.Helpers;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Tests.Controllers
{
    [TestFixture]
    public class AnswerControllerTests
    {
        [Test]
        public void Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModel = new AnswerViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.GetAnswer(It.IsAny<int>())).Returns(viewModel);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Text"), viewModel.Text);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        }

        [Test]
        public void Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.GetAnswer(It.IsAny<int>())).Returns<Answer>(null);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void All_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModels = new List<AnswerViewModel>
            {
                new AnswerViewModel
                {
                    Id = 1,
                    Text = "Text1"
                },
                new AnswerViewModel
                {
                    Id = 2,
                    Text = "Text2"
                }
            };

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.GetAnswers(It.IsAny<int>())).Returns(viewModels);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.All(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Text, viewModels.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Id, viewModels.First().Id);
        }

        [Test]
        public void All_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.GetAnswers(It.IsAny<int>())).Returns<List<AnswerViewModel>>(null);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.All(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new AnswerViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.CreateAnswer(It.IsAny<AnswerViewModel>())).Returns(viewModel);


            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Put(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IAnswerRepository>();

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new AnswerViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.UpdateAnswer(It.IsAny<AnswerViewModel>())).Returns(viewModel);

            var controller =
                new AnswerController(mockRepo.Object);

            var result = controller.Post(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IAnswerRepository>();

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Post_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var viewModel = new AnswerViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.UpdateAnswer(It.IsAny<AnswerViewModel>())).Returns<AnswerViewModel>(null);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Post(viewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            int id = 1;

            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.DeleteAnswer(It.IsAny<int>())).Returns(true);

            var controller =
                new AnswerController(mockRepo.Object);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var mockRepo = new Mock<IAnswerRepository>();
            mockRepo.Setup(x => x.DeleteAnswer(1)).Returns(false);

            var controller = new AnswerController(mockRepo.Object);

            var result = controller.Delete(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
