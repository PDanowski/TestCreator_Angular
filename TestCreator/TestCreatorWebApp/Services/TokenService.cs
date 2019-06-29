using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.Data.Models;
using TestCreatorWebApp.Helpers;
using TestCreatorWebApp.ViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TestCreatorWebApp.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Claim[] CreateClaims(string userId)
        {
            DateTime now = DateTime.UtcNow;

            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
            };
        }

        public Token GenerateRefreshToken(string clientId, string userId)
        {
            return new Token
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreationDate = DateTime.Now
            };
        }

        public TokenData CreateAccessToken(string userId)
        {
            DateTime now = DateTime.UtcNow;

            var claims = CreateClaims(userId);

            var tokenExpirationMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(
                    issuerSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenData
            {
                EncodedToken = encodedToken,
                ExporationTimeInMinutes = tokenExpirationMins,
            };
        }

        public TokenData CreateSecurityToken(Claim[] claims)
        {
            DateTime now = DateTime.UtcNow;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));
            var tokenExpirationalMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");

            var token = new JwtSecurityToken(issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationalMins)),
                signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256));

            return new TokenData
            {
                EncodedToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExporationTimeInMinutes = tokenExpirationalMins
            };
        }
    }
}
