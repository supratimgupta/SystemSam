using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSam.Core.Constants
{
    /// <summary>
    /// Communication modes
    /// </summary>
    public enum COMMUNICATION_MODE
    {
        SYSTEM_WILL_TALK=1,
        SYSTEM_TALKED = 2,
        SYSTEM_ADVICED = 3,
        SYSTEM_NOTIFIED = 4,
        SYSTEM_WAITING_FOR_COMMAND = 5,
        SYSTEM_RECIEVED_COMMAND = 6,
        SYSTEM_EXECUTED_COMMAND = 7,
        USER_WILL_TALK = 8,
        USER_GAVE_COMMAND = 9
    }
}
