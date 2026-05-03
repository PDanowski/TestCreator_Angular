using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Constants;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.WebApp.Mappers;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserAndRoleRepository _userAndRoleRepository;
        private readonly IAppMapper _mapper;

        public UserController(IUserAndRoleRepository userAndRoleRepository, IAppMapper mapper)
        {
            _userAndRoleRepository = userAndRoleRepository;
            _mapper = mapper;
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
                return new BadRequestResult();
            }

            try
            {
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

                var createdUser = await _userAndRoleRepository.CreateUserAndAddToRolesAsync(
                    _mapper.ToModel(viewModel),
                    new[] {UserRoles.RegisteredUser});

                return Json(_mapper.ToViewModel(createdUser), JsonSettings);
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
