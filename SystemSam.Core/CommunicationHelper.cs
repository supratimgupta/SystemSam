using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSam.Core
{
    /// <summary>
    /// Singleton class to get current communication mode
    /// </summary>
    public sealed class CommunicationHelper
    {
        /// <summary>
        /// Private object
        /// </summary>
        static CommunicationHelper _communicationHelper;

        /// <summary>
        /// Lock object
        /// </summary>
        static object _lock = new object();

        /// <summary>
        /// Current communication mode
        /// </summary>
        static Constants.COMMUNICATION_MODE _communicationMode;

        /// <summary>
        /// Private constructor
        /// </summary>
        CommunicationHelper()
        {

        }

        /// <summary>
        /// Communication object
        /// </summary>
        public static CommunicationHelper CommunicationObject
        {
            get
            {
                lock(_lock)
                {
                    if (_communicationHelper == null)
                    {
                        _communicationMode = Constants.COMMUNICATION_MODE.SYSTEM_WILL_TALK;
                        _communicationHelper = new CommunicationHelper();
                    }
                }
                return _communicationHelper;
            }
        }

        /// <summary>
        /// Current communication mode
        /// </summary>
        public Constants.COMMUNICATION_MODE CurrentCommunicationMode
        {
            set
            {
                _communicationMode = value;
            }
            get
            {
                return _communicationMode;
            }
        }
    }
}
