namespace Cake.EntityFramework6.Tests
{
    using Cake.EntityFramework6.Interfaces;

    using Xunit.Abstractions;

    internal class MockLogger : ILogger
    {
        private readonly ITestOutputHelper _helper;

        public MockLogger(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        public void Warning(string message)
        {
            _helper.WriteLine($"{nameof(Warning)}: {message}");
        }

        public void Information(string message)
        {
            _helper.WriteLine($"{nameof(Information)}: {message}");
        }

        public void Error(string message)
        {
            _helper.WriteLine($"{nameof(Error)}: {message}");
        }

        public void Debug(string message)
        {
            _helper.WriteLine($"{nameof(Debug)}: {message}");
        }

        public void Verbose(string message)
        {
            _helper.WriteLine($"{nameof(Verbose)}: {message}");
        }
    }
}