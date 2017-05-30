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
            /*
            const string sql = @"IF  EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'SchoolDb')
                                BEGIN
	                                ALTER DATABASE SchoolDb  SET OFFLINE WITH ROLLBACK IMMEDIATE
	                                DROP DATABASE SchoolDb
                                END";

            using (var sqlCnn = new SqlConnection(SqlServerFactConstants.InstanceConnectionString))
            {
                sqlCnn.Open();
                var cmd = sqlCnn.CreateCommand();

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            */
        }
    }

    [CollectionDefinition(Traits.SqlServerCollection)]
    public class SqlServerFixtureFixtureCollection : ICollectionFixture<SqlServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<PostgresFixture> interfaces.
    }
}
