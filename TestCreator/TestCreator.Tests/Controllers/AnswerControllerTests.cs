using System.Collections.Generic;
using System.Linq;
using Mapster;
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
    public class AnswerControllerTests
    {
        private Mock<IAnswerRepository> _mockRepo;

        private AnswerController _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockRepo = new Mock<IAnswerRepository>();
            _sut =  new AnswerController(_mockRepo.Object);
        }

        [Test]
        public void Get_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            var answerId = 1;
            var answer = new Answer
            {
                Id = answerId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.GetAnswer(answerId)).Returns(answer);

            var result = _sut.Get(answerId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Text"), answer.Text);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), answer.Id);
        }

        [Test]
        public void Get_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var answerId = 2;
            _mockRepo.Setup(x => x.GetAnswer(answerId)).Returns<Answer>(null);

            var result = _sut.Get(answerId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void GetByQuestionId_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            var questionId = 1;
            var answers = new List<Answer>
            {
                new Answer
                {
                    Id = 1,
                    Text = "Text1",
                    QuestionId = questionId
                },
                new Answer
                {
                    Id = 2,
                    Text = "Text2",
                    QuestionId = questionId
                }
            };

            _mockRepo.Setup(x => x.GetAnswers(questionId)).Returns(answers);

            var result = _sut.GetByQuestionId(questionId) as JsonResult;

            Assert.IsNotNull(result);

            var viewModelsCollection = result.GetIEnumberableFromJsonResult<AnswerViewModel>().ToList();
            foreach (var answer in answers)
            {
                Assert.IsTrue(viewModelsCollection.Any(x => x.Text == answer.Text));
                Assert.IsTrue(viewModelsCollection.Any(x => x.Id == answer.Id));
                Assert.IsTrue(viewModelsCollection.Any(x => x.QuestionId == questionId));
            }
        }

        [Test]
        public void GetByQuestionId_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var questionId = 2;
            _mockRepo.Setup(x => x.GetAnswers(questionId)).Returns<List<Answer>>(null);

            var result = _sut.GetByQuestionId(questionId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var answer = new Answer
            {
                Id = 1,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.CreateAnswer(It.IsAny<Answer>())).Returns(answer);

            var result = _sut.Post(answer.Adapt<AnswerViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            var viewModel = result.GetObjectFromJsonResult<AnswerViewModel>();
            Assert.AreEqual(viewModel.Text, answer.Text);
            Assert.AreEqual(viewModel.Id, answer.Id);
        }

        [Test]
        public void Post_WhenInvalidViewModelGiven_ShouldReturnStatusCode500()
        {
            var result = _sut.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var answerId = 1;
            var answer = new Answer
            {
                Id = answerId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.UpdateAnswer(It.Is<Answer>(a => a.Id == answerId))).Returns(answer);

            var result = _sut.Put(answer.Adapt<AnswerViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            var viewModel = result.GetObjectFromJsonResult<AnswerViewModel>();
            Assert.AreEqual(viewModel.Text, answer.Text);
            Assert.AreEqual(viewModel.Id, answer.Id);
        }

        [Test]
        public void Put_WhenInvalidViewModelGiven_ShouldReturnStatusCode500()
        {
            var result = _sut.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Put_WhenCorrectViewModelErrorDuringProcessing_ShouldReturnNotFound()
        {
            var answerId = 2;
            var answer = new Answer
            {
                Id = answerId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.UpdateAnswer(It.Is<Answer>(a => a.Id == answerId))).Returns<Answer>(null);

            var result = _sut.Put(answer.Adapt<AnswerViewModel>());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var answerId = 1;

            _mockRepo.Setup(x => x.DeleteAnswer(answerId)).Returns(true);

            var result = _sut.Delete(answerId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelErrorDuringProcessing_ShouldReturnNotFound()
        {
            var answerId = 2;

            _mockRepo.Setup(x => x.DeleteAnswer(answerId)).Returns(false);

            var result = _sut.Delete(answerId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
