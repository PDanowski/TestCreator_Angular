using System.Linq;
using TestCreator.Data.Context;
using TestCreator.Data.Models;
using TestCreator.Data.Repositories.Interfaces;

namespace TestCreator.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Token CheckRefreshTokenForClient(string clientId, string refreshToken)
        {
            return _context.Tokens.FirstOrDefault(t => t.ClientId == clientId && t.Value == refreshToken);
        }

        public void RemoveRefreshToken(Token refreshToken)
        {
            _context.Tokens.Remove(refreshToken);
            _context.SaveChanges();
        }


        public void AddRefreshToken(Token refreshToken)
        {
            _context.Tokens.Add(refreshToken);
            _context.SaveChanges();
        }
    }
}
