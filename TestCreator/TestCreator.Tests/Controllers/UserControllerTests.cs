using System.Threading.Tasks;
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
    public class UserControllerTests
    {
        [Test]
        public void Post_CorrectViewModelGivenUserDoesntExist_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };

            var mockRepo = new Mock<IUserAndRoleRepository>();
            mockRepo.Setup(x => x.CreateUserAndAddToRolesAsync(It.IsAny<UserViewModel>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(viewModel));
            mockRepo.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));
            mockRepo.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));

            var controller = new UserController(mockRepo.Object);

            var result = controller.Post(viewModel).Result as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<UserViewModel>().Email, viewModel.Email);
            Assert.AreEqual(result.GetObjectFromJsonResult<UserViewModel>().UserName, viewModel.UserName);
        }

        [Test]
        public void Post_CorrectViewModelGivenUserWithNameExists_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };
            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };

            var mockRepo = new Mock<IUserAndRoleRepository>();
            mockRepo.Setup(x => x.CreateUserAndAddToRolesAsync(It.IsAny<UserViewModel>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(viewModel));
            mockRepo.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockRepo.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));

            var controller = new UserController(mockRepo.Object);

            var result = controller.Post(viewModel).Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Post_CorrectViewModelGivenUserWithEmailExists_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };
            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };

            var mockRepo = new Mock<IUserAndRoleRepository>();
            mockRepo.Setup(x => x.CreateUserAndAddToRolesAsync(It.IsAny<UserViewModel>(), It.IsAny<string[]>()))
                .Returns(Task.FromResult(viewModel));
            mockRepo.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));
            mockRepo.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            var controller = new UserController(mockRepo.Object);

            var result = controller.Post(viewModel).Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockRepo = new Mock<IUserAndRoleRepository>();

            var controller = new UserController(mockRepo.Object);

            var result = controller.Post(null).Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
