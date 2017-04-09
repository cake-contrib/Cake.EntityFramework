#addin nuget:?package=Cake.EntityFramework6

var migrationSettings = new EfMigratorSettings {
  AssemblyPath = @"bin\debug\Cake.EntityFramework.TestProject.Postgres.exe",
  ConfigurationClass = "Cake.EntityFramework.TestProject.Postgres.Migrations.Configuration",
  AppConfigPath = @"bin\debug\Cake.EntityFramework.TestProject.Postgres.exe.config",
  ConnectionString = "Host=127.0.0.1; Database=cake_dev; Username=postgres; Password=Password12!;",
  ConnectionProvider = "Npgsql" // <- using PostgreSQL here
};

Task("Default")
  .Description("Migrate database to latest.")
  .Does(() =>
{
  using(var migrator = CreateEfMigrator(migrationSettings)){
    var localMigrations = migrator.GetLocalMigrations();
    var lastGoodMigration = localMigrations.OrderByDescending(x => x).Skip(1).First();
    migrator.MigrateTo(lastGoodMigration);
    migrator.Commit();
  }
});

var target = Argument("target", "Default");
RunTarget(target);
