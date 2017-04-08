using System;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework6.Tests.Migrator.Postgres
{
    public static class PostgresFactConstants
    {
        public static readonly string DdlPath = $@"{AssemblyDirectory}\Postgres\Cake.EntityFramework.TestProject.Postgres.dll";
        public const string ConfigName = "Cake.EntityFramework.TestProject.Postgres.Migrations.Configuration";
        public static readonly string AppConfig = $@"{AssemblyDirectory}\Postgres\Cake.EntityFramework.TestProject.Postgres.dll.config";

        public static string InstanceConnectionString =>
            $"Host=127.0.0.1; Database=cake_dev_{Guid.NewGuid().ToString("N")}; Username=postgres; Password=Password12!;";

        public const string ConnectionProvider = "Npgsql";

        // http://stackoverflow.com/a/283917
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}