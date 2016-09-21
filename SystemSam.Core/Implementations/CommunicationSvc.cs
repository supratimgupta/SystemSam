using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemSam.Config.Contracts;
using SystemSam.Config.Implementations;
using SystemSam.Core.Contracts;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace SystemSam.Core.Implementations
{
    public class CommunicationSvc : Contracts.ICommunicationSvc,IDisposable
    {
        #region PrivateVariables

        IConfigSvc _configSvc;

        ICommunicationSvc _communicationSvc;

        SpeechSynthesizer _reader;

        DTOs.CommunicationDTO _communicationDTO;

        CommunicationHelper _helper;

        SpeechRecognitionEngine _recognizer;

        string _userCommand;

        public event Delegates.UserSpokenHandler OnUserSpoken;

        public event Delegates.UserSpeechNotRecognizedHandler OnUserSpeechRejected;

        ManualResetEvent _completed = null;

        List<string> _lstAlternates;

        #endregion

        #region Constructors
        public CommunicationSvc()
        {
            _configSvc = new ConfigSvc();
            _communicationSvc = new CommunicationSvc();
            _reader = new SpeechSynthesizer();
            _recognizer = new SpeechRecognitionEngine();
            _reader.SpeakStarted += _reader_SpeakStarted;
            _reader.SpeakProgress += _reader_SpeakProgress;
            _reader.SpeakCompleted += _reader_SpeakCompleted;
            _helper = CommunicationHelper.CommunicationObject;
            _userCommand = string.Empty;
            
            _completed = new ManualResetEvent(false);
            _recognizer.SpeechRecognitionRejected+=_recognizer_SpeechRecognitionRejected;
            _recognizer.SpeechRecognized+=_recognizer_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice(); // set the input of the speech recognizer to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            _completed.WaitOne(); // wait until speech recognition is completed
            
        }

        public CommunicationSvc(IConfigSvc configSvc)
        {
            _configSvc = configSvc;
            _communicationSvc = new CommunicationSvc();
            _reader = new SpeechSynthesizer();
            _recognizer = new SpeechRecognitionEngine();
            _reader.SpeakStarted += _reader_SpeakStarted;
            _reader.SpeakProgress += _reader_SpeakProgress;
            _reader.SpeakCompleted += _reader_SpeakCompleted;
            _helper = CommunicationHelper.CommunicationObject;
            _userCommand = string.Empty;

            _completed = new ManualResetEvent(false);
            _recognizer.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice(); // set the input of the speech recognizer to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            _completed.WaitOne(); // wait until speech recognition is completed
        }

        public CommunicationSvc(IConfigSvc configSvc, ICommunicationSvc communicationSvc)
        {
            _configSvc = configSvc;
            _communicationSvc = communicationSvc;
            _reader = new SpeechSynthesizer();
            _recognizer = new SpeechRecognitionEngine();
            _reader.SpeakStarted += _reader_SpeakStarted;
            _reader.SpeakProgress += _reader_SpeakProgress;
            _reader.SpeakCompleted += _reader_SpeakCompleted;
            _helper = CommunicationHelper.CommunicationObject;
            _userCommand = string.Empty;

            _completed = new ManualResetEvent(false);
            _recognizer.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice(); // set the input of the speech recognizer to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            _completed.WaitOne(); // wait until speech recognition is completed
        }

        #endregion

        #region EventHandlers

        
        void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result.Alternates.Count > 0)
            {
                _lstAlternates = new List<string>();
                foreach (RecognizedPhrase r in e.Result.Alternates)
                {
                    _lstAlternates.Add(r.Text);
                }
            }
            //Raise rejected event
            OnUserSpeechRejected();
            _completed.Set();
        }

        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _userCommand = e.Result.Text;
            //Raise success event
            OnUserSpoken();
            _completed.Set();
        }

        void _reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if(_communicationDTO!=null && _communicationDTO.CallbackSpeakCompleted!=null)
            {
                _communicationDTO.CallbackSpeakCompleted();
            }
        }

        void _reader_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            if(_communicationDTO!=null && _communicationDTO.CallbackSpeakProgress!=null)
            {
                _communicationDTO.CallbackSpeakProgress();
            }
        }

        void _reader_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            if(_communicationDTO!=null && _communicationDTO.CallbackSpeakStarted!=null)
            {
                _communicationDTO.CallbackSpeakStarted();
            }
        }

        #endregion

        #region PublicMethods
        /// <summary>
        /// System talks
        /// </summary>
        /// <param name="communicationDTO">Communication DTO</param>
        public void SystemCommunicates(DTOs.CommunicationDTO communicationDTO)
        {
            _communicationDTO = communicationDTO;
            _reader.Speak(communicationDTO.Message);
            _helper.CurrentCommunicationMode = communicationDTO.NextCommunicationMode;
        }

        /// <summary>
        /// Gets user command
        /// </summary>
        /// <returns>User command</returns>
        public string GetUserCommand()
        {
            return _userCommand;
        }

        /// <summary>
        /// Executes user command
        /// </summary>
        /// <param name="communicationDTO">Communication DTO</param>
        public void ExecuteUserCommand(DTOs.CommunicationDTO communicationDTO)
        {
            if(communicationDTO!=null && communicationDTO.CallbackCommand!=null)
            {
                communicationDTO.CallbackCommand();
            }
        }

        /// <summary>
        /// Loads grammar to recognizer
        /// </summary>
        /// <param name="grammar">Grammar</param>
        public void LoadGrammarToRecognizer(Grammar grammar)
        {
            if(_recognizer!=null)
            {
                _recognizer.LoadGrammar(grammar);
            }
        }

        /// <summary>
        /// Removes grammar from recognition
        /// </summary>
        /// <param name="grammarName">Grammar name</param>
        public void RemoveGrammarFromRecognizer(string grammarName)
        {
            if(_recognizer!=null && _recognizer.Grammars!=null)
            {
                foreach (Grammar gr in _recognizer.Grammars)
                {
                    if (string.Equals(gr.Name,grammarName, StringComparison.OrdinalIgnoreCase))
                    {
                        _recognizer.UnloadGrammar(gr);
                        break;
                    }
                } 
            }
        }

        /// <summary>
        /// Pauses reader
        /// </summary>
        public void PauseReader()
        {
            if (_reader.State == SynthesizerState.Speaking)
            {
                _reader.Pause();
            }
        }

        /// <summary>
        /// Resumes reader
        /// </summary>
        public void ResumeReader()
        {
            if (_reader.State == SynthesizerState.Paused)
            {
                _reader.Resume();
            }
        }

        /// <summary>
        /// Disposes
        /// </summary>
        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
            _reader = null;
            if(_recognizer!=null)
            {
                _recognizer.Dispose();
            }
            _recognizer = null;
        }
        #endregion
    }
}
