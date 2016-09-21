using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSam.Config.Contracts
{
    public interface IConfigSvc
    {
        string GetRAMThreshold();

        string GetHDDThreshold();
    }
}
