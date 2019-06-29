using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Data;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.ViewModels;

namespace TestCreatorWebApp.Repositories
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
