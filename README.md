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

> TODO make a Nuget package. 

```
Install-Package Cake.EntityFramework
```

Alternatively, add directly to a cake script via the `#addin` directive.

> TODO see above.

```
#addin "Cake.EntityFramework"
```

## Usage

```cake
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

## Recommendations

Run migrations in trasactions. Keep in mind that this only works with supported providers (e.g. basiclly everyone except MySQL and Oracle). For example in PostgreSQL (Npgsql) you would do:

```c#
public class AtomicMigrationScriptBuilder : NpgsqlMigrationSqlGenerator
{
    public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
    {
        yield return new MigrationStatement { Sql = "BEGIN TRANSACTION" };

        foreach (var migrationStatement in base.Generate(migrationOperations, providerManifestToken))
        {
            yield return migrationStatement;
        }

        yield return new MigrationStatement { Sql = "COMMIT TRANSACTION" };
    }
}
```

This will allow for more reliable rollbacks. 

## How it works

> TODO Write

## TODO

* Add documentation (e.g. XML-Docs, Cakedocs)
* Handle errors better
* Test with more EF providers
* More tests in general
* EF 7 support
* Add a build server
* Add nuget packages
* Refactor repository
