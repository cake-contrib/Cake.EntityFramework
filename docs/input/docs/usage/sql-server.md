---
Order: 15
Title: SQL Server
---

Requirements:
- SQL Server 2014 (localdb) or Higher
- Modify connection string if necessary

### Example
```cake
var migrationSettings = new EfMigratorSettings {
  AssemblyPath = @"path/to/example.data.dll",
  ConfigurationClass = "Example.Migrations.Configuation",
  AppConfigPath = @"path/to/example.data.dll.config",
  ConnectionString = "Server=(localdb)\MSSQLLocalDB;Database=SchoolDb;User ID=sa;Password=Password12!",
  ConnectionProvider = "System.Data.SqlClient" // <- using SQL Server LocalDb here
};

//Migration Example
Task("Migrate-To_Latest")
  .Description("Migrate database to latest.")
  .Does(() =>
{
  using(var migrator = CreateEfMigrator(migrationSettings)){
    migrator.MigrateToLatest();
    migrator.Commit();
  }
});


//Generate Script Example
Task("Generate-Script-To_Latest")
  .Description("Generates a script to latest migration.")
  .Does(() =>
{
  using(var migrator = CreateEfMigrator(migrationSettings))
  {
      var script = migrator.GenerateScriptForLatest();
      Information(script);
  }
});

```