using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace SystemSam.Core.Contracts
{
    /// <summary>
    /// Communication Svc
    /// </summary>
    public interface ICommunicationSvc
    {
        /// <summary>
        /// System communicates
        /// </summary>
        /// <param name="communicationDTO">Communication DTO</param>
        void SystemCommunicates(DTOs.CommunicationDTO communicationDTO);

        /// <summary>
        /// Pauses reader
        /// </summary>
        void PauseReader();

        /// <summary>
        /// Resumes reader
        /// </summary>
        void ResumeReader();

        /// <summary>
        /// Gets user command
        /// </summary>
        /// <returns>User command</returns>
        string GetUserCommand();

        /// <summary>
        /// Loads grammar to recognizer
        /// </summary>
        /// <param name="grammar">Grammar</param>
        void LoadGrammarToRecognizer(Grammar grammar);

        /// <summary>
        /// Removes grammar from recognizer
        /// </summary>
        /// <param name="grammarName">Grammar name</param>
        void RemoveGrammarFromRecognizer(string grammarName);

        /// <summary>
        /// Executes user command
        /// </summary>
        /// <param name="communicationDTO">Communication DTO</param>
        void ExecuteUserCommand(DTOs.CommunicationDTO communicationDTO);
    }
}
