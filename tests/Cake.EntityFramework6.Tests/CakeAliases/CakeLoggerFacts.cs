namespace Cake.EntityFramework6.Tests.CakeAliases
{
    using Cake.Core.Diagnostics;
    using Cake.EntityFramework6.CakeTranslation;
    using Cake.EntityFramework6.Interfaces;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class CakeLoggerFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();

        private readonly ILogger _cakeLogger;
        private readonly ICakeLog _log;
        private readonly string _message;

        public CakeLoggerFacts()
        {
            _log = Substitute.For<ICakeLog>();
            _message = AutoFixture.Create<string>();
            _cakeLogger = new CakeLogger(_log);
        }

        [Fact]
        public void Info_log_should_log_to_info()
        {
            // Act
            _cakeLogger.Information(_message);

            // Assert
            _log.Received().Information(_message);
        }

        [Fact]
        public void Debug_log_should_log_to_debug()
        {
            // Act
            _cakeLogger.Debug(_message);

            // Assert
            _log.Received().Debug(_message);
        }

        [Fact]
        public void Error_log_should_log_to_error()
        {
            // Act
            _cakeLogger.Error(_message);

            // Assert
            _log.Received().Error(_message);
        }

        [Fact]
        public void Verbose_log_should_log_to_verbose()
        {
            // Act
            _cakeLogger.Verbose(_message);

            // Assert
            _log.Received().Verbose(_message);
        }

        [Fact]
        public void Warn_log_should_log_to_warn()
        {
            // Act
            _cakeLogger.Warning(_message);

            // Assert
            _log.Received().Warning(_message);
        }
    }
}