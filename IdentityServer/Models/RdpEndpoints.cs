
namespace IdentityServer.Models
{
    public class RdpEndpoints
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public Domain Domain { get; set; }
    }
}