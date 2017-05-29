namespace Cake.EntityFramework.Interfaces
{
    /// <summary>
    /// Logger contract
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs warnings such as use of deprecated APIs, poor use of API, 'almost' errors, other runtime situations
        /// that are undesirable or unexpected, but not necessarily "wrong".
        /// </summary>
        /// <param name="message">Message to log</param>
        void Warning(string message);

        /// <summary>
        /// Logs information or 'interesting' runtime events.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Information(string message);

        /// <summary>
        /// Logs errors or other runtime errors or unexpected conditions.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Error(string message);

        /// <summary>
        /// Logs debug information which includes the most detailed information.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Debug(string message);

        /// <summary>
        /// Logs verbose or detailed information on the flow through the system.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Verbose(string message);
    }
}