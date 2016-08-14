namespace Cake.EntityFramework6.Models
{
    using System;

    [Serializable]
    public class MigrationResult
    {
        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }

        public MigrationResult(bool isSuccess, Exception exception = null)
        {
            IsSuccess = isSuccess;
            Exception = exception;
        }

        public MigrationResult()
        {
        }
    }
}