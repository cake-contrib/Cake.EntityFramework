using System;
using Cake.Core;
using Xunit;
using System.IO;

namespace Cake.EntityFramework.Tests.Fixtures
{

    public sealed class SqlServerFixture : IDisposable
    {
        private const string _DockerFilePath = "../../../../docker/sql-server/docker-compose.yml";

        public SqlServerFixture()
        {
            Initialize();
        }

        public string SqlServerDockerComposeFilePath { get; private set; }
        public ICakeContext CakeContext { get; private set; }

        private void Initialize()
        {
            SqlServerDockerComposeFilePath = Path.GetFullPath(_DockerFilePath);

            CakeContext = new CakeContextFixture();

            if (!Util.IsRunningBuildServer())
                Util.StartDockerComposeProcess(SqlServerDockerComposeFilePath, "up", 5000);
        }

        public void Dispose()
        {
            //Stop Docker Container
            if (!Util.IsRunningBuildServer())
                Util.StartDockerComposeProcess(SqlServerDockerComposeFilePath, "down");
        }
    }

    [CollectionDefinition(Traits.SqlServerCollection)]
    public class SqlServerFixtureFixtureCollection : ICollectionFixture<PostgresFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<PostgresFixture> interfaces.
    }
}
