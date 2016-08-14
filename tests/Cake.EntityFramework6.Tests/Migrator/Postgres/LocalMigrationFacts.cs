namespace Cake.EntityFramework6.Tests.Migrator.Postgres
{
    using System;

    using Cake.EntityFramework6.Actors;
    using Cake.EntityFramework6.Interfaces;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class LocalMigrationFacts
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ILogger _mockLogger;
        private readonly IEfMigrator _migrator;

        public LocalMigrationFacts(ITestOutputHelper logHelper)
        {
            _mockLogger = new MockLogger(logHelper);
            _migrator = new EfMigrator(PostgresFactConstants.DdlPath, PostgresFactConstants.ConfigName, PostgresFactConstants.AppConfig,
                PostgresFactConstants.InstanceConnectionString, PostgresFactConstants.ConnectionProvider, _mockLogger);
        }

        [Fact]
        public void Can_get_local_migrations()
        {
            using (_migrator)
            {
                var migrations = _migrator.GetLocalMigrations();
                migrations.Should().HaveCount(8);
            }
        }
    }
}