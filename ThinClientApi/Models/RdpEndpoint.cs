

using System;

namespace ThinClientApi.Models
{
    [Serializable]
    public class RdpEndpoint
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
    }
}