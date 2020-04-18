using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace TestCreatorWebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {

        }

        public string Token { get; set; }
        public int Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
