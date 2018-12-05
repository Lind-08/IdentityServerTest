using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models
{
    public class Domain
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; } 
        public ICollection<RdpEndpoints> RdpEndpoints { get; set; }
    }
}
