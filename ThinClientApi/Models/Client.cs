using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinClientApi.Models
{
    public static class Client
    {
        public static string GetLastVersion() => "1.0";

        internal static bool CheckVersion(string version)
        {
            if (version != GetLastVersion())
                return false;
            else
                return true;
        }

        internal static string GetActualReference() => "ActualReference";
    }
}
