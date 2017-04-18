using System;
using Cake.Core;
using Xunit;
using System.IO;
using System.Diagnostics;

namespace Cake.EntityFramework.Tests.Fixtures
{

    public sealed class PostgresFixture : IDisposable
    {
        private const string _DockerFilePath = "../../../../docker/postgres/docker-compose.yml";

        public PostgresFixture()
        {
            Initialize();
        }

        public string PostGresDockerComposeFilePath { get; private set; }
        public ICakeContext CakeContext { get; private set; }

        private void Initialize()
        {
            PostGresDockerComposeFilePath = Path.GetFullPath(_DockerFilePath);
            CakeContext = new CakeContextFixture();

            Util.StartDockerComposeProcess(PostGresDockerComposeFilePath, "up", 5000);
        }

        public void Dispose()
        {
            //Stop Docker Container
            Util.StartDockerComposeProcess(PostGresDockerComposeFilePath, "down");
        }
    }

    [CollectionDefinition(Traits.PostgresCollection)]
    public class PostgresFixtureCollection : ICollectionFixture<PostgresFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<PostgresFixture> interfaces.
    }
}
