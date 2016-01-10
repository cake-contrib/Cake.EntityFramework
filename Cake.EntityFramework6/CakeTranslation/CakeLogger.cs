namespace Cake.EntityFramework6.CakeTranslation
{
    using Cake.Core.Diagnostics;
    using Cake.EntityFramework6.Contracts;

    public class CakeLogger : ILogger
    {
        private readonly ICakeLog _cakeLogger;

        public CakeLogger(ICakeLog cakeLogger)
        {
            _cakeLogger = cakeLogger;
        }

        public void Warning(string message)
        {
            _cakeLogger.Warning(message);
        }

        public void Information(string message)
        {
            _cakeLogger.Information(message);
        }

        public void Error(string message)
        {
            _cakeLogger.Error(message);
        }

        public void Debug(string message)
        {
            _cakeLogger.Debug(message);
        }

        public void Verbose(string message)
        {
            _cakeLogger.Verbose(message);
        }
    }
}
