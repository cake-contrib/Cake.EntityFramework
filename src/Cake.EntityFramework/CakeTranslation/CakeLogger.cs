using Cake.Core.Diagnostics;
using Cake.EntityFramework.Interfaces;

namespace Cake.EntityFramework.CakeTranslation
{
    /// <summary>
    /// Cake Logger implmentation
    /// </summary>
    public class CakeLogger : ILogger
    {
        private readonly ICakeLog _cakeLogger;

        /// <summary>
        /// Cake logger implementation
        /// </summary>
        /// <param name="cakeLogger">Ilogger from cake.core</param>
        public CakeLogger(ICakeLog cakeLogger)
        {
            _cakeLogger = cakeLogger;
        }

        /// <summary>
        /// Logs warnings such as use of deprecated APIs, poor use of API, 'almost' errors, other runtime situations
        /// that are undesirable or unexpected, but not necessarily "wrong".
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Warning(string message)
        {
            _cakeLogger.Warning(message);
        }

        /// <summary>
        /// Logs information or 'interesting' runtime events.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Information(string message)
        {
            _cakeLogger.Information(message);
        }

        /// <summary>
        /// Logs errors or other runtime errors or unexpected conditions.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Error(string message)
        {
            _cakeLogger.Error(message);
        }

        /// <summary>
        /// Logs debug information which includes the most detailed information.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Debug(string message)
        {
            _cakeLogger.Debug(message);
        }

        /// <summary>
        /// Logs verbose or detailed information on the flow through the system.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Verbose(string message)
        {
            _cakeLogger.Verbose(message);
        }
    }
}