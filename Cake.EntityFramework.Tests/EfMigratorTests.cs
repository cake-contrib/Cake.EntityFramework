namespace Cake.EntityFramework.Tests
{
    using Cake.EntityFramework6.Actors;
    using Cake.EntityFramework6.Contracts;

    using FluentAssertions;

    using Xunit;
    using Xunit.Abstractions;

    public class EfMigratorTests
    {
        private readonly ILogger _mockLogger;

        public EfMigratorTests(ITestOutputHelper logHelper)
        {
            _mockLogger = new MockLogger(logHelper);
        }

        private const string DdlPath = @"..\..\..\Cake.EntityFramework.TestProject.Postgres\bin\Debug\Cake.EntityFramework.TestProject.Postgres.exe";
        private const string ConfigName = "Cake.EntityFramework.TestProject.Postgres.Migrations.Configuration";
        private const string AppConfig = @"..\..\..\Cake.EntityFramework.TestProject.Postgres\bin\Debug\Cake.EntityFramework.TestProject.Postgres.exe.config";

        private const string ConnectionString = "Host=127.0.0.1; Database=cake_dev; Username=dev; Password=dev;";
        private const string ConnectionProvider = "Npgsql";

        [Fact]
        public void Can_get_local_migrations()
        {
            using (var migrator = new EfMigrator(DdlPath, ConfigName, AppConfig, ConnectionString, ConnectionProvider, _mockLogger))
            {
                var migrations = migrator.GetLocalMigrations();
                migrations.Should().HaveCount(8);
            }
        }

        [Fact]
        public void Can_get_remote_migrations()
        {
            using (var migrator = new EfMigrator(DdlPath, ConfigName, AppConfig, ConnectionString, ConnectionProvider, _mockLogger))
            {
                var migrations = migrator.GetRemoteMigrations();
                migrations.Should().HaveCount(2);
            }
        }

        [Fact]
        public void Current_migration_should_be_correct()
        {
            using (var migrator = new EfMigrator(DdlPath, ConfigName, AppConfig, ConnectionString, ConnectionProvider, _mockLogger))
            {
                var migrations = migrator.GetCurrentMigration();
                migrations.Should().Be("201601100159523_V2");
            }
        }

        [Fact]
        public void Latest_migration_should_be_correct()
        {
            using (var migrator = new EfMigrator(DdlPath, ConfigName, AppConfig, ConnectionString, ConnectionProvider, _mockLogger))
            {
                var migrations = migrator.GetLatestMigration();
                migrations.Should().Be("201601100203055_Bad");
            }
        }

        [Fact]
        public void Can_get_pending_migrations()
        {
            using (var migrator = new EfMigrator(DdlPath, ConfigName, AppConfig, ConnectionString, ConnectionProvider, _mockLogger))
            {
                var migrations = migrator.GetPendingMigrations();
                migrations.Should().HaveCount(6);
            }
        }
    }
}
