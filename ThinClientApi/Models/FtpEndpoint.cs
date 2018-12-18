using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinClientApi.Models
{
    public class FtpEndpoint
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
