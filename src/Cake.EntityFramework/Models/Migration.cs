using System;

namespace Cake.EntityFramework.Models
{
    /// <summary>
    /// Migration class 
    /// </summary>
    [Serializable]
    public class Migration
    {
        /// <summary>
        /// Name of the Migration
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets whether the migraton completed
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Gets or Sets wheter the migration errored out
        /// </summary>
        public bool Error { get; set; }
        
        /// <summary>
        /// If the exception errored out, the details of the exception.
        /// </summary>
        public Exception Exception { get; set; }
    }
}