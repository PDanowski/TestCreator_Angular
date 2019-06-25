using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestCreatorWebApp.Abstract;
using TestCreatorWebApp.ViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TestCreatorWebApp.Controllers
{
    public class TokenController : BaseApiController
    {
        private readonly IUserAndRoleRepository _repository;
        private readonly ITokenService _tokenService;

        public TokenController(IUserAndRoleRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody]TokenRequestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            switch (viewModel.GrantType)
            {
                case "password":
                    return await GetToken(viewModel);
                default:
                    return new UnauthorizedResult();
            }
        }

        public async Task<IActionResult> GetToken(TokenRequestViewModel viewModel)
        {
            try
            {
                var user = await _repository.GetUserByNameAsync(viewModel.Username);

                if (user == null && viewModel.Username.Contains("@"))
                {
                    user = await _repository.GetUserByEmailAsync(viewModel.Username);
                }

                if (user == null || !await _repository.CheckPasswordAsync(user, viewModel.Password))
                {
                    return new UnauthorizedResult();
                }

                var claims = _tokenService.CreateClaims(user.Id);

                var tokenData = _tokenService.CreateSecurityToken(claims);

                var response = new TokenResponseViewModel
                {
                    Token = tokenData.EncodedToken,
                    Expiration = tokenData.ExporationTimeInMinutes
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
