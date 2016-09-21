using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSam.Core.Delegates;

namespace SystemSam.Core.DTOs
{
    /// <summary>
    /// Communication DTO
    /// </summary>
    public class CommunicationDTO
    {
        /// <summary>
        /// Message to talk
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Next communication mode
        /// </summary>
        public Constants.COMMUNICATION_MODE NextCommunicationMode { get; set; }

        /// <summary>
        /// Current communication mode
        /// </summary>
        public Constants.COMMUNICATION_MODE CurrentCommunicationMode { get; set; }

        /// <summary>
        /// Callback command when user commands
        /// </summary>
        public CallbackCommand CallbackCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Callback speak completed
        /// </summary>
        public CallbackSpeakCompleted CallbackSpeakCompleted
        {
            get;
            set;
        }

        /// <summary>
        /// Callback speak progress
        /// </summary>
        public CallbackSpeakProgress CallbackSpeakProgress
        {
            get;
            set;
        }
        
        /// <summary>
        /// Callback speak started
        /// </summary>
        public CallbackSpeakStarted CallbackSpeakStarted
        {
            get;
            set;
        }
    }
}
