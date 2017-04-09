# Build Script

You can reference Cake.EntityFramework in your build script as a cake addin:

```cake
#addin "Cake.EntityFramework6"
```

or nuget reference:

```cake
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.EntityFramework6"
```

Then some examples:

## PostgreSQL

Requires:
- Windows 10 Pro
- HyperV installed
- Docker for Windows

```
cd docker\postgres
docker-compose start
```

### Cake Script Example
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

## MS SQL Server

*Coming Soon*