using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.Data.Models;

namespace TestCreatorWebApp.Abstract
{
    public interface IUserAndRoleRepository
    {
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
