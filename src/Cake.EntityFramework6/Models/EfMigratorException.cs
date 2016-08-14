namespace Cake.EntityFramework6.Models
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class EfMigrationException : Exception
    {
        public EfMigrationException()
        {
        }

        public EfMigrationException(string message) : base(message)
        {
        }

        public EfMigrationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EfMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}