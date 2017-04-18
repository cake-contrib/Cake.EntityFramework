using System;

namespace Cake.EntityFramework.Models
{
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