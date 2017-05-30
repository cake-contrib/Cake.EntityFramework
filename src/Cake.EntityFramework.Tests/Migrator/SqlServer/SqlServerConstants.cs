using System;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework.Tests.Migrator.SqlServer
{
    public static class SqlServerFactConstants
    {
        public static readonly string DdlPath = $@"{ (!Util.IsRunningBuildServer() ? AssemblyDirectory : Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER") + "/BuildArtifacts/temp/_PublishedxUnitTests/Cake.EntityFramework.Tests")}\Cake.EntityFramework.TestProject.SqlServer.dll";
        public const string ConfigName = "Cake.EntityFramework.TestProject.SqlServer.Migrations.Configuration";
        public static readonly string AppConfig = $@"{(!Util.IsRunningBuildServer() ? AssemblyDirectory : Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER") + "/BuildArtifacts/temp/_PublishedLibraries/Cake.EntityFramework.TestProject.SqlServer")}\Cake.EntityFramework.TestProject.SqlServer.dll.config";

        public static string InstanceConnectionString => $@"Server={ (!Util.IsRunningBuildServer() ? "(localdb)\\MSSQLLocalDB" : "(local)\\SQL2016") };Database=SchoolDb;User ID=sa;Password=Password12!";

        public const string ConnectionProvider = "System.Data.SqlClient";

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