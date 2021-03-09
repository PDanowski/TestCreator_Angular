using TestCreator.Data.Models;

namespace TestCreator.Data.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Token CheckRefreshTokenForClient(string clientId, string refreshToken);
        void RemoveRefreshToken(Token refreshToken);
        void AddRefreshToken(Token refreshToken);
        
    }
}
