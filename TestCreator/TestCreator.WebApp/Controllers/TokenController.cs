﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Repositories.Interfaces;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class TokenController : BaseApiController
    {
        private readonly IUserAndRoleRepository _userAndRoleRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenService _tokenService;

        public TokenController(IUserAndRoleRepository userAndRoleRepository, 
            ITokenRepository tokenRepository, ITokenService tokenService)
        {
            _userAndRoleRepository = userAndRoleRepository;
            _tokenRepository = tokenRepository;
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
                case "refresh_token":
                    return await RefreshToken(viewModel);
                default:
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestViewModel viewModel)
        {
            try
            {
                var refreshToken =
                    _tokenRepository.CheckRefreshTokenForClient(viewModel.ClientId, viewModel.RefreshToken);

                if (refreshToken == null)
                {
                    return new UnauthorizedResult();
                }

                var user = await _userAndRoleRepository.GetUserById(refreshToken.UserId);

                if (user == null)
                {
                    return new UnauthorizedResult();
                }

                var newRefreshToken = _tokenService.GenerateRefreshToken(refreshToken.ClientId, refreshToken.UserId);
                _tokenRepository.RemoveRefreshToken(refreshToken);
                _tokenRepository.AddRefreshToken(newRefreshToken);

                var tokenData = _tokenService.CreateAccessToken(newRefreshToken.UserId);

                var response = new TokenResponseViewModel
                {
                    Expiration = tokenData.ExporationTimeInMinutes,
                    RefreshToken = newRefreshToken.Value,
                    Token = tokenData.EncodedToken
                };

                return Json(response);

            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }
 
        private async Task<IActionResult> GetToken(TokenRequestViewModel viewModel)
        {
            try
            {
                var user = await _userAndRoleRepository.GetUserByNameAsync(viewModel.Username);

                if (user == null && viewModel.Username.Contains("@"))
                {
                    user = await _userAndRoleRepository.GetUserByEmailAsync(viewModel.Username);
                }

                if (user == null || !await _userAndRoleRepository.CheckPasswordAsync(user, viewModel.Password))
                {
                    return new UnauthorizedResult();
                }

                var token = _tokenService.GenerateRefreshToken(viewModel.ClientId, user.Id);

                _tokenRepository.AddRefreshToken(token);

                var accessTokenData = _tokenService.CreateAccessToken(user.Id);

                var response = new TokenResponseViewModel
                {
                    Token = accessTokenData.EncodedToken,
                    Expiration = accessTokenData.ExporationTimeInMinutes,
                    RefreshToken = token.Value
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
