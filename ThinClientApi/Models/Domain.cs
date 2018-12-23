using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThinClientApi.Models
{
    public class Domain
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RdpEndpoint> RdpEndpoints { get; set; }
        public ICollection<FtpServerEndpoint> FtpEndpoints { get; set; }
    }
}
