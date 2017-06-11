using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Migrator;
using Cake.EntityFramework.TestProject.SqlServer;
using Cake.EntityFramework.Tests.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Cake.EntityFramework.Tests.Migrator.SqlServer
{
    public class RemoteMigrationsFacts : IDisposable
    {
        private readonly ITestOutputHelper _logHelper;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ILogger _mockLogger;
        private SchoolContext _DbContext;
        private readonly IEfMigrator _migrator;

        public RemoteMigrationsFacts(ITestOutputHelper logHelper)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);
            _migrator = new EfMigrator(SqlServerFactConstants.DdlPath, SqlServerFactConstants.ConfigName, SqlServerFactConstants.AppConfig,
                SqlServerFactConstants.InstanceConnectionString, SqlServerFactConstants.ConnectionProvider, _mockLogger, false);
        }
        
        [Fact]
        public void When_no_remote_migrations_current_migration_should_return_not_empty_or_null()
        {
            var migrations = _migrator.GetCurrentMigration();
            migrations.Should().NotBeNullOrEmpty();
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

        public void Dispose()
        {
        }
    }
}