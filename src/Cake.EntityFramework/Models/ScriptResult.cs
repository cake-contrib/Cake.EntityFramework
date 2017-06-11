using Cake.Core.IO;
using System;

namespace Cake.EntityFramework.Models
{
    /// <summary>
    /// Migration Result 
    /// </summary>
    [Serializable]
    public class ScriptResult
    {
        /// <summary>
        /// Gets or Sets whether the result was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Gets or sets the exception if the script generation was not successful
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the script text generated from the migration
        /// </summary>
        public string Script { get; private set; }

        /// <summary>
        /// Script Result
        /// </summary>
        /// <param name="isSuccess"> whether the result was successful</param>
        /// <param name="script">if successful, the actual migration script</param>
        /// <param name="exception">the exception if the migration was not successful, defaults to null</param>
        public ScriptResult(bool isSuccess, string script, Exception exception = null)
        {
            IsSuccess = isSuccess;
            Script = script;
            Exception = exception;
        }
    }
}