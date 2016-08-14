namespace Cake.EntityFramework6.Models
{
    using System;

    [Serializable]
    public class Migration
    {
        public string Name { get; set; }

        public bool Completed { get; set; }

        public bool Error { get; set; }

        public Exception Exception { get; set; }
    }
}
