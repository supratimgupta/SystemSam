using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SystemSam.Config.Implementations
{
    public class ConfigSvc : Contracts.IConfigSvc
    {
        public string GetRAMThreshold()
        {
            return ConfigurationManager.AppSettings["RAM_THRESHOLD"];
        }

        public string GetHDDThreshold()
        {
            return ConfigurationManager.AppSettings["HDD_THRESHOLD"];
        }
    }
}
