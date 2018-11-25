using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThinClientApi.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace ThinClientApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class VersionController : ControllerBase
    {
        // GET /version
        [HttpGet]
        public IActionResult Get(string version)
        {
            Dictionary<string, string> responce;
            if (!Client.CheckVersion(version))
            {
                responce = new Dictionary<string, string>
                {
                    { "LastVersion", Client.GetLastVersion() },
                    { "Reference", Client.GetActualReference() }
                };
            }
            else
            {
                responce = new Dictionary<string, string>
                {
                    { "Status", "OK" }
                };
            }
            return new JsonResult(responce);
        }
    }
}
