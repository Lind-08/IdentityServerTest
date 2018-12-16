using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThinClientApi.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using ThinClientApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ThinClientApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class VersionController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; } 

        public VersionController(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        // GET /version
        [HttpGet("{version}", Name = "Check")]
        public IActionResult CheckVersion(string version)
        {
            var lastFile = DbContext.ClientFiles.Last();
            Dictionary<string, string> responce;
            if (!lastFile.CheckVersion(version))
            {
                responce = new Dictionary<string, string>
                {
                    { "LastVersion", lastFile.Version },
                    { "Reference", lastFile.FileName },
                    { "Checksum", lastFile.Checksum }
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
