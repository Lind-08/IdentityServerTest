﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThinClientApi.Data;
using ThinClientApi.Models;

namespace ThinClientApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class ConnectionController : ControllerBase
    {
        private class GetConnectionResponce
        {
            public ICollection<RdpEndpoint> RdpEndpoints;
            public ICollection<FtpServerEndpoint> FtpEndpoints;

            public GetConnectionResponce(Domain domain)
            {
                RdpEndpoints = domain.RdpEndpoints;
                FtpEndpoints = domain.FtpEndpoints;
            }
        }

        ApplicationDbContext DbContext { get; set; }
        public ConnectionController(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }
        // GET: /Connection
        [HttpGet]
        public IActionResult Get()
        {
            var domainName = (from c in User.Claims where c.Type == "domain" select c.Value).FirstOrDefault();
            if (domainName != null)
            {
                var domains = DbContext.Domains.Include(t => t.RdpEndpoints ).Include(t => t.FtpEndpoints).ToList();
                var domain = domains.Where(c => c.Name == domainName).FirstOrDefault();
                if (domain != null)
                {
                    return new JsonResult(new GetConnectionResponce(domain));
                }
                else
                {
                    return new JsonResult(new Dictionary<string, string> { { "Error", "Domain not found" } });
                }
            }
            else
            {
                return new JsonResult(new Dictionary<string, string> { { "Error", "Domain not founded in token" } });
            }
        }
    }
}
