namespace Cake.EntityFramework6.Tests.Migrator
{
    using System;

    using Cake.EntityFramework6.Interfaces;
    using Cake.EntityFramework6.Migrator;
    using Cake.EntityFramework6.Models;
    using Cake.EntityFramework6.Tests.Migrator.Postgres;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;
    using Xunit.Abstractions;

    public class GenericMigratorFacts
    {
        private readonly ITestOutputHelper _logHelper;
        private static readonly Fixture AutoFixture = new Fixture();
        private readonly ILogger _mockLogger;

        public GenericMigratorFacts(ITestOutputHelper logHelper)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);
        }

        [Fact]
        public void When_appConfig_path_should_not_exist_throw()
        {
            var appPath = AutoFixture.Create<string>();
            Action action = () => new EfMigrator(null, null, appPath, null, null, _mockLogger);

            // Act

            // Assert
            var exception = action.ShouldThrow<EfMigrationException>().Which;
            _logHelper.WriteLine(exception.Message);
        }

        [Fact]
        public void When_assembly_path_should_not_exist_throw()
        {
            var appPath = PostgresFactConstants.AppConfig;
            var assemblyPath = AutoFixture.Create<string>();
            Action action = () => new EfMigrator(assemblyPath, null, appPath, null, null, _mockLogger);

            // Act

            // Assert
            var exception = action.ShouldThrow<EfMigrationException>().Which;
            _logHelper.WriteLine(exception.Message);
        }

        [Fact]
        public void When_db_config_does_not_exist_should_throw()
        {
            var appPath = PostgresFactConstants.AppConfig;
            var assemblyPath = PostgresFactConstants.DdlPath;
            var dbConfig = AutoFixture.Create<string>();
            Action action = () => new EfMigrator(assemblyPath, dbConfig, appPath, null, null, _mockLogger);

            // Act

            // Assert
            var exception = action.ShouldThrow<EfMigrationException>().Which;
            _logHelper.WriteLine(exception.Message);
        }

        [Fact]
        public void When_connection_string_is_incorrect_throw()
        {
            var appPath = PostgresFactConstants.AppConfig;
            var assemblyPath = PostgresFactConstants.DdlPath;
            var dbConfig = PostgresFactConstants.ConfigName;
            var connectionString = AutoFixture.Create<string>();
            var providerName = PostgresFactConstants.ConnectionProvider;
            Action action = () => new EfMigrator(assemblyPath, dbConfig, appPath, connectionString, providerName, _mockLogger);

            // Act

            // Assert
            var exception = action.ShouldThrow<Exception>().Which;
            _logHelper.WriteLine(exception.Message);
        }
    }
}