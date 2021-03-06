﻿using System.Collections.Generic;
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
    public class ResultControllerTests
    {
        private Mock<IResultRepository> _mockRepo;

        private ResultController _sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockRepo = new Mock<IResultRepository>();
            _sut = new ResultController(_mockRepo.Object);
        }

        [Test]
        public void Get_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            var resultId = 1;
            var resultModel = new Result
            {
                Id = resultId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.GetResult(resultId)).Returns(resultModel);

            var result = _sut.Get(resultId) as JsonResult;

            Assert.IsNotNull(result);
            var viewModel = result.GetObjectFromJsonResult<ResultViewModel>();
            Assert.AreEqual(viewModel.Text, resultModel.Text);
            Assert.AreEqual(viewModel.Id, resultModel.Id);
        }

        [Test]
        public void Get_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var resultId = 2;
            _mockRepo.Setup(x => x.GetResult(resultId)).Returns<Result>(null);

            var result = _sut.Get(resultId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void GetByTestId_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            var testId = 1;
            var results = new List<Result>
            {
                new Result
                {
                    Id = 1,
                    Text = "Text1",
                    TestId = testId
                },
                new Result
                {
                    Id = 2,
                    Text = "Text2",
                    TestId = testId
                }
            };

            _mockRepo.Setup(x => x.GetResults(testId)).Returns(results);

            var result = _sut.GetByTestId(testId) as JsonResult;

            Assert.IsNotNull(result);

            var viewModelsCollection = result.GetIEnumberableFromJsonResult<ResultViewModel>().ToList();
            foreach (var resultModel in results)
            {
                Assert.IsTrue(viewModelsCollection.Any(x => x.Text == resultModel.Text));
                Assert.IsTrue(viewModelsCollection.Any(x => x.Id == resultModel.Id));
                Assert.IsTrue(viewModelsCollection.Any(x => x.TestId == testId));
            }
        }

        [Test]
        public void GetByTestId_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var testId = 2;
            _mockRepo.Setup(x => x.GetResults(testId)).Returns<List<ResultViewModel>>(null);

            var result = _sut.GetByTestId(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var resultId = 1;
            var resultModel = new Result
            {
                Id = resultId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.CreateResult(It.Is<Result>(r => r.Id == resultId))).Returns(resultModel);

            var result = _sut.Post(resultModel.Adapt<ResultViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            var viewModel = result.GetObjectFromJsonResult<ResultViewModel>();
            Assert.AreEqual(viewModel.Text, resultModel.Text);
            Assert.AreEqual(viewModel.Id, resultModel.Id);
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
            var resultId = 1;
            var resultModel = new Result
            {
                Id = resultId,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.UpdateResult(It.Is<Result>(r => r.Id == resultId))).Returns(resultModel);

            var result = _sut.Put(resultModel.Adapt<ResultViewModel>()) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, resultModel.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, resultModel.Id);
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
            var viewModel = new ResultViewModel
            {
                Id = 1,
                Text = "Text1"
            };

            _mockRepo.Setup(x => x.UpdateResult(viewModel.Adapt<Result>())).Returns<ResultViewModel>(null);

            var result = _sut.Put(viewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelGiven_ShouldReturnJsonViewModel()
        {
            var resultId = 1;

            _mockRepo.Setup(x => x.DeleteResult(resultId)).Returns(true);

            var result = _sut.Delete(resultId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_WhenCorrectViewModelErrorDuringProcessing_ShouldReturnNotFound()
        {
            var resultId = 2;

            _mockRepo.Setup(x => x.DeleteResult(resultId)).Returns(false);

            var result = _sut.Delete(resultId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
