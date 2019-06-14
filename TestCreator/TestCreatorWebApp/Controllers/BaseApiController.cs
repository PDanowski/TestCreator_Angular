using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestCreatorWebApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        protected JsonSerializerSettings JsonSettings;

        public BaseApiController()
        {
            JsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        } 
}
}
