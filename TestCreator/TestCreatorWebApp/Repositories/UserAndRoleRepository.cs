using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Data.Models;

namespace TestCreatorWebApp.Repositories
{
    public class UserAndRoleRepository : IUserAndRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAndRoleRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
    }
}
