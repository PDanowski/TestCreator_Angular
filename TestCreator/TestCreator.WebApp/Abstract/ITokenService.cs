using System.Security.Claims;
using TestCreator.WebApp.Data.Models;
using TestCreator.WebApp.Helpers;

namespace TestCreator.WebApp.Abstract
{
    public interface ITokenService
    {
        Claim[] CreateClaims(string userId);
        TokenData CreateSecurityToken(Claim[] claims);
        Token GenerateRefreshToken(string clientId, string userId);
        TokenData CreateAccessToken(string userId);
    }
}
