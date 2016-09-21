using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSam.Core.Delegates
{
    /// <summary>
    /// Callback command from user
    /// </summary>
    public delegate void CallbackCommand();

    /// <summary>
    /// Speak completed callback
    /// </summary>
    public delegate void CallbackSpeakCompleted();

    public delegate void CallbackSpeakProgress();

    public delegate void CallbackSpeakStarted();

    public delegate void UserSpokenHandler();

    public delegate void UserSpeechNotRecognizedHandler();
}
