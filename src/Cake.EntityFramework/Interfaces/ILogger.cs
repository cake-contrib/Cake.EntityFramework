namespace Cake.EntityFramework.Interfaces
{
    public interface ILogger
    {
        void Warning(string message);
        void Information(string message);
        void Error(string message);
        void Debug(string message);
        void Verbose(string message);
    }
}