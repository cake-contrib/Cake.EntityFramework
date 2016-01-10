namespace Cake.EntityFramework6.Models
{
    using Cake.Core.IO;

    public class EfMigratorSettings
    {
        public DirectoryPath AssemblyPath { get; set; }

        public string ConfigurationClass { get; set; }

        public DirectoryPath AppConfigPath { get; set; }

        public string ConnectionString { get; set; }

        public string ConnectionProvider { get; set; }
    }
}