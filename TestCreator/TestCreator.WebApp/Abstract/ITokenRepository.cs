using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface ITokenRepository
    {
        Token CheckRefreshTokenForClient(string clientId, string refreshToken);
        void RemoveRefreshToken(Token refreshToken);
        void AddRefreshToken(Token refreshToken);
        
    }
}
