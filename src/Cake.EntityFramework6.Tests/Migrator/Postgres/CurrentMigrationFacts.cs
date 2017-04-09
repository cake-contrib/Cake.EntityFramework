using System.Linq;

using Cake.EntityFramework6.Interfaces;
using Cake.EntityFramework6.Migrator;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace Cake.EntityFramework6.Tests.Migrator.Postgres
{
    public class CurrentMigrationFacts
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ITestOutputHelper _logHelper;

        private readonly ILogger _mockLogger;

        private readonly string _instanceString = PostgresFactConstants.InstanceConnectionString;

        private IEfMigrator Migrator => new EfMigrator(PostgresFactConstants.DdlPath, PostgresFactConstants.ConfigName, PostgresFactConstants.AppConfig,
            _instanceString, PostgresFactConstants.ConnectionProvider, _mockLogger);

        public CurrentMigrationFacts(ITestOutputHelper logHelper)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);

            _logHelper.WriteLine($"Using connectionString: {_instanceString}");
        }

        [Fact]
        public void When_no_remote_migration_current_migration_should_return_null()
        {
            var migrator = Migrator;

            // Act
            var result = migrator.GetCurrentMigration();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void When_there_remote_migration_current_migration_should_match()
        {
            var setupMigrator = Migrator;
            var firstMigration = setupMigrator.GetLocalMigrations().First();
            setupMigrator.MigrateTo(firstMigration);
            setupMigrator.Commit();

            var migrator = Migrator;

            // Act
            var result = migrator.GetCurrentMigration();

            // Assert
            result.Should().Be(firstMigration);
        }
    }
}