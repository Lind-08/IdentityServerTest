using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models
{
    public class RdpEndpoints
    {
        [Key]
        public string Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        [ForeignKey("Domain")]
        public string DomainId { get; set; }
        public Domain Domain { get; set; }
    }
}