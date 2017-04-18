using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Migrator;
using Cake.EntityFramework.Tests.Fixtures;
using FluentAssertions;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Cake.EntityFramework.Tests.Migrator.Postgres
{
    [Collection(Traits.PostgresCollection)]
    public class RemoteMigrationsFacts
    {
        private readonly ITestOutputHelper _logHelper;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ILogger _mockLogger;

        private readonly IEfMigrator _migrator;
        private readonly PostgresFixture _postGresFixture;

        public RemoteMigrationsFacts(ITestOutputHelper logHelper, PostgresFixture postgresFixture)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);
            _migrator = new EfMigrator(PostgresFactConstants.DdlPath, PostgresFactConstants.ConfigName, PostgresFactConstants.AppConfig,
                PostgresFactConstants.InstanceConnectionString, PostgresFactConstants.ConnectionProvider, _mockLogger, false);
            _postGresFixture = postgresFixture;
        }

        [Fact]
        public void When_no_remote_migration_remote_migrations_should_be_zero()
        {
            var migrations = _migrator.GetRemoteMigrations();
            migrations.Should().HaveCount(0);
        }

        [Fact]
        public void When_no_remote_migrations_current_migration_should_return_null()
        {
            var migrations = _migrator.GetCurrentMigration();
            migrations.Should().BeNull();
        }

        [Fact]
        public void When_single_remote_migration_current_migration_should_return_single_migration()
        {
            var singleMigration = _migrator.GetLocalMigrations().First();

            _migrator.MigrateTo(singleMigration);

            var migrations = _migrator.GetCurrentMigration();
            migrations.Should().Be(singleMigration);
        }

        [Fact]
        public void Remote_migrations_should_be_in_descending_order()
        {
            var lastGoodMigration = _migrator.GetLocalMigrations().Skip(1).First();
            _migrator.MigrateTo(lastGoodMigration);

            // Act
            var migrations = _migrator.GetRemoteMigrations()
                                      .ToList();

            foreach (var migration in migrations)
            {
                _logHelper.WriteLine(migration);
            }

            // Assert
            migrations.Should().BeInDescendingOrder();
        }
    }
}