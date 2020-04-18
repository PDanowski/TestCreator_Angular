using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.Helpers
{
    public struct TokenData
    {
        public string EncodedToken { get; set; }
        public int ExporationTimeInMinutes { get; set; }
    }
}
