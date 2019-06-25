using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCreatorWebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenRequestViewModel
    {
        public TokenRequestViewModel()
        {
                
        }

        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
