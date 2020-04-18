using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.Helpers;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Abstract
{
    public interface ITokenService
    {
        Claim[] CreateClaims(string userId);
        TokenData CreateSecurityToken(Claim[] claims);
        Token GenerateRefreshToken(string clientId, string userId);
        TokenData CreateAccessToken(string userId);
    }
}
