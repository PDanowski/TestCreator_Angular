using System.Threading.Tasks;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Abstract
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
