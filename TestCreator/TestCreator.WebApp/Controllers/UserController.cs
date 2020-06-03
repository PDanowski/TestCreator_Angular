using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.Constants;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserAndRoleRepository _userAndRoleRepository;

        public UserController(IUserAndRoleRepository userAndRoleRepository)
        {
            _userAndRoleRepository = userAndRoleRepository;
        }

        /// <summary>
        /// POST: api/user/put
        /// </summary>
        /// <param name="viewModel">UserViewModel with data</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            ApplicationUser user = await _userAndRoleRepository.GetUserByNameAsync(viewModel.UserName);
            if (user != null)
            {
                return BadRequest("User with given username already exists");
            }

            user = await _userAndRoleRepository.GetUserByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return BadRequest("User with given e-mail already exists");
            }

            var createdUser = await _userAndRoleRepository.CreateUserAndAddToRolesAsync(viewModel, new[] {UserRoles.RegisteredUser});

            return Json(createdUser, JsonSettings);
        }
    }
}
