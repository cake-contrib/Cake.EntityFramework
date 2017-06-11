using System;
using Cake.Core;
using Xunit;
using Cake.EntityFramework.Tests.Migrator.SqlServer;
using Cake.EntityFramework.TestProject.SqlServer;
using System.Data.SqlClient;

namespace Cake.EntityFramework.Tests.Fixtures
{
    public sealed class SqlServerFixture : IDisposable
    {
        public SqlServerFixture()
        {
            Initialize();
        }

        public string SqlServerDockerComposeFilePath { get; private set; }
        public ICakeContext CakeContext { get; private set; }

        private void Initialize()
        {
            CakeContext = new CakeContextFixture();
        }
        
        public void Dispose()
        {
        }
    }

    [CollectionDefinition(Traits.SqlServerCollection)]
    public class SqlServerFixtureFixtureCollection : ICollectionFixture<SqlServerFixture>
    {
    }
}
