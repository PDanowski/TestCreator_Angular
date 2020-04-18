using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using TestCreator.WebApp.Abstract;
using TestCreator.WebApp.Data;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Repositories
{
    public class UserAndRoleRepository : IUserAndRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserAndRoleRepository(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<ApplicationUser> GetUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<UserViewModel> CreateUserAndAddToRolesAsync(UserViewModel viewModel, string[] roles)
        {
            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                DisplayName = viewModel.DisplayName,
                CreationDate = DateTime.Now,
                LastModificationDate = DateTime.Now
            };

            await _userManager.CreateAsync(user);

            await _userManager.AddToRolesAsync(user, roles);

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            _context.SaveChanges();

            return user.Adapt<UserViewModel>();
        }
    }
}
