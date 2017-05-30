using System.Linq;
using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Migrator;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Cake.EntityFramework.Tests.Fixtures;

namespace Cake.EntityFramework.Tests.Migrator.SqlServer
{
    //[Collection(Traits.SqlServerCollection)]
    public class LocalMigrationFacts
    {
        private readonly ITestOutputHelper _logHelper;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ILogger _mockLogger;

        private readonly IEfMigrator _migrator;

        public LocalMigrationFacts(ITestOutputHelper logHelper)//, SqlServerFixture sqlServerFixture)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);
            _migrator = new EfMigrator(SqlServerFactConstants.DdlPath, SqlServerFactConstants.ConfigName, SqlServerFactConstants.AppConfig,
                SqlServerFactConstants.InstanceConnectionString, SqlServerFactConstants.ConnectionProvider, _mockLogger, false);
        }

        [Fact]
        public void When_migrations_exist_locally_return_list()
        {
            var migrations = _migrator.GetLocalMigrations()
                                      .ToList();

            foreach (var migration in migrations)
            {
                _logHelper.WriteLine(migration);
            }

            migrations.Should().HaveCount(8);
        }

        [Fact]
        public void Local_migrations_should_be_in_ascending_order()
        {
            var migrations = _migrator.GetLocalMigrations()
                                      .ToList();

            foreach (var migration in migrations)
            {
                _logHelper.WriteLine(migration);
            }

            migrations.Should().BeInAscendingOrder();
        }

        [Fact]
        public void Latest_migration_should_be_latest_local()
        {
            var localMigrations = _migrator.GetLocalMigrations().ToList();

            // Act
            var result = _migrator.GetLatestMigration();

            // Assert
            result.Should().Be(localMigrations.Last());
        }
        
        [Fact]
        public void When_pending_migrations_exist_has_pening_should_be_true()
        {
            // Act
            var result = _migrator.HasPendingMigrations();

            // Assert
            result.Should().BeTrue();
        }
    }
}