using System;

namespace Cake.EntityFramework.Models
{
    /// <summary>
    /// Migration Result 
    /// </summary>
    [Serializable]
    public class MigrationResult
    {
        /// <summary>
        /// Gets or Sets whether the result was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the exception if the migration was not successful
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Migration Result
        /// </summary>
        /// <param name="isSuccess"> whether the result was successful</param>
        /// <param name="exception">the exception if the migration was not successful, defaults to null</param>
        public MigrationResult(bool isSuccess, Exception exception = null)
        {
            IsSuccess = isSuccess;
            Exception = exception;
        }

        /// <summary>
        /// Migration Result
        /// </summary>
        public MigrationResult()
        { }
    }
}