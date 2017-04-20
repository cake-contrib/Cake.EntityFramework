using System;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework.Tests.Migrator.Postgres
{
    public static class PostgresFactConstants
    {
        private static readonly string AppVeyorArtifactPath = "BuildArtifacts/temp/_PublishedxUnitTests/Cake.EntityFramework.Tests";

        public static readonly string DdlPath = $@"{AssemblyDirectory}\Cake.EntityFramework.TestProject.Postgres.dll";
        public const string ConfigName = "Cake.EntityFramework.TestProject.Postgres.Migrations.Configuration";
        public static readonly string AppConfig = $@"{AssemblyDirectory}\Cake.EntityFramework.TestProject.Postgres.dll.config";

        public static string InstanceConnectionString =>
            $"Host=127.0.0.1; Database=cake_dev_{Guid.NewGuid().ToString("N")}; Username=postgres; Password=Password12!;";

        public const string ConnectionProvider = "Npgsql";

        // http://stackoverflow.com/a/283917
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = string.Empty;

                if (Util.IsRunningBuildServer())
                    codeBase = codeBase.Substring(0, codeBase.IndexOf("src/", StringComparison.OrdinalIgnoreCase)) + AppVeyorArtifactPath;
                else
                    codeBase = Assembly.GetExecutingAssembly().CodeBase;

                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}