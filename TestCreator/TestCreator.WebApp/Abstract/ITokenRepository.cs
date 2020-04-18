using TestCreator.WebApp.Data.Models;

namespace TestCreator.WebApp.Abstract
{
    public interface ITokenRepository
    {
        Token CheckRefreshTokenForClient(string clientId, string refreshToken);
        void RemoveRefreshToken(Token refreshToken);
        void AddRefreshToken(Token refreshToken);
        
    }
}
