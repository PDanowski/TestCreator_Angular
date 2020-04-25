using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class QuestionControllerTests
    {
        [Test]
        public void Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModel = new QuestionViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.GetQuestion(It.IsAny<int>())).Returns(viewModel);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.GetQuestion(It.IsAny<int>())).Returns<Question>(null);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void All_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var viewModels = new List<QuestionViewModel>
            {
                new QuestionViewModel
                {
                    Id = 1,
                    Text = "Text1"
                },
                new QuestionViewModel
                {
                    Id = 2,
                    Text = "Text2"
                }
            };

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.GetQuestions(It.IsAny<int>())).Returns(viewModels);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.All(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Text, viewModels.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Id, viewModels.First().Id);
        }

        [Test]
        public void All_InvalidIdGiven_ReturnsNotFound()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.GetQuestions(It.IsAny<int>())).Returns<List<QuestionViewModel>>(null);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.All(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new QuestionViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.CreateQuestion(It.IsAny<QuestionViewModel>())).Returns(viewModel);


            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Post(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IQuestionRepository>();

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = new QuestionViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.UpdateQuestion(It.IsAny<QuestionViewModel>())).Returns(viewModel);

            var controller =
                new QuestionController(mockRepo.Object);

            var result = controller.Put(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, viewModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, viewModel.Id);
        }

        [Test]
        public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IQuestionRepository>();

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var viewModel = new QuestionViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.UpdateQuestion(It.IsAny<QuestionViewModel>())).Returns<QuestionViewModel>(null);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Put(viewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            int id = 1;

            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.DeleteQuestion(It.IsAny<int>())).Returns(true);

            var controller =
                new QuestionController(mockRepo.Object);

            var result = controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var mockRepo = new Mock<IQuestionRepository>();
            mockRepo.Setup(x => x.DeleteQuestion(1)).Returns(false);

            var controller = new QuestionController(mockRepo.Object);

            var result = controller.Delete(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
