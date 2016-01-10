# Cake.EntityFramework

> This is considered alpha quality, use at your own risk.

A set of Cake aliases for Entity Framework code-first migrations.

## Implemented functionality

* Get local migrations
* Get remote migrations
* Get pending migrations (local - remote migrations)
* Current migration
* Latest possible migration
* Migrate to version (up or down)
* Migrate to latest
* Basic transactional support (rollback on error)

## Using

`Cake.EntityFramework` is available as a Nuget package. Install from the Package Management Console.
```
Install-Package Cake.EntityFramework
```

Alternatively, add directly to a cake script via the `#addin` directive.
```
#addin "Cake.EntityFramework"
```

## Usage

```
#addin "Cake.EntityFramework"

var migrationSettings = new EfMigratorSettings {
  AssemblyPath = "path/to/example.data.dll",
  ConfigurationClass = "Example.Migrations.Configuation",
  AppConfigPath = "path/to/example.data.dll.config",
  ConnectionString = "Host=127.0.0.1; Database=cake_dev; Username=dev; Password=dev;",
  ConnectionProvider = "Npgsql" // <- using PostgreSQL here
}

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

## TODO

* Add documentation (e.g. XML-Docs)
* Handle errors better
* Test with more EF providers
* More tests in general
* EF 7 support
* Add a build server
