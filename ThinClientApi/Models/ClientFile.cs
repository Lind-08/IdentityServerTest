using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinClientApi.Models
{
    public class ClientFile
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
        public string Checksum { get; set; }

        internal bool CheckVersion(string version)
        {
            if (version != Version)
                return false;
            else
                return true;
        }
    }
}
