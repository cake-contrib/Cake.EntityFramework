using System;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework.Tests.Migrator.Postgres
{
    public static class PostgresFactConstants
    {
        public static readonly string DdlPath = $@"{ (!Util.IsRunningBuildServer() ? AssemblyDirectory : Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER") + "/BuildArtifacts/temp/_PublishedxUnitTests/Cake.EntityFramework.Tests")}\Cake.EntityFramework.TestProject.Postgres.dll";
        public const string ConfigName = "Cake.EntityFramework.TestProject.Postgres.Migrations.Configuration";
        public static readonly string AppConfig = $@"{(!Util.IsRunningBuildServer() ? AssemblyDirectory : Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER") + "/BuildArtifacts/temp/_PublishedLibraries/Cake.EntityFramework.TestProject.Postgres")}\Cake.EntityFramework.TestProject.Postgres.dll.config";

        public static string InstanceConnectionString =>
            $"Host=127.0.0.1; Database=cake_dev_{Guid.NewGuid().ToString("N")}; Username=postgres; Password=Password12!;";

        public const string ConnectionProvider = "Npgsql";

        // http://stackoverflow.com/a/283917
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}