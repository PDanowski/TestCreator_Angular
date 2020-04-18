using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface IUserAndRoleRepository
    {
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserById(string userId);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<UserViewModel> CreateUserAndAddToRolesAsync(UserViewModel viewModel, string[] roles);
    }
}
