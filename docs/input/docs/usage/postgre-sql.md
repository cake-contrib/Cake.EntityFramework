---
Order: 20
Title: PostgreSQL
---

Requirements:
- Windows 10 Pro
- HyperV installed
- Docker for Windows

```
cd docker\postgres
docker-compose start
```

### Example
```cake
var migrationSettings = new EfMigratorSettings {
  AssemblyPath = @"path/to/example.data.dll",
  ConfigurationClass = "Example.Migrations.Configuation",
  AppConfigPath = @"path/to/example.data.dll.config",
  ConnectionString = "Host=127.0.0.1; Database=cake_dev; Username=postgres; Password=Password12!;",
  ConnectionProvider = "Npgsql" // <- using PostgreSQL here
};

Task("Migrate-To_Latest")
  .Description("Migrate database to latest.")
  .Does(() =>
{
  using(var migrator = CreateEfMigrator(migrationSettings)){
    migrator.MigrateToLatest();
    migrator.Commit();
  }
});
```