![icon](docs/images/icon.png)

# Cake.EntityFramework

[![AppVeyor](https://img.shields.io/appveyor/ci/Silvenga/cake-entityframework.svg)](https://ci.appveyor.com/project/Silvenga/cake-entityframework)

> This is considered alpha quality, use at your own risk.

A set of Cake aliases for Entity Framework code-first migrations.

## Implemented functionality

### EF6

- [X] Get local migrations
- [X] Get remote migrations
- [X] Get pending migrations (local - remote migrations)
- [X] Get current migration
- [X] Get latest pending migration
- [X] Migrate to version (up or down)
- [X] Migrate to latest version pending
- [X] Basic transactional support (rollback on error)

### EFCore1

- [ ] Get local migrations
- [ ] Get remote migrations
- [ ] Get pending migrations (local - remote migrations)
- [ ] Get current migration
- [ ] Get latest pending migration
- [ ] Migrate to version (up or down)
- [ ] Migrate to latest version pending
- [ ] Basic transactional support (rollback on error)

## Using

`Cake.EntityFramework6` is available as a Nuget package. Install from the Package Management Console.

> TODO make a Nuget package. 

```
Install-Package Cake.EntityFramework6
```

Alternatively, add directly to a cake script via the `#addin` directive.

> TODO see above.

```
#addin "Cake.EntityFramework6"
```

## Usage

```cake
#addin "Cake.EntityFramework6"

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

- [ ] Add documentation (e.g. XML-Docs, Cakedocs)
- [X] Handle errors better
- [ ] Test with more EF providers
- [X] More tests in general
- [ ] EF 7 support
- [X] Add a build server
- [ ] Add nuget packages
- [X] Refactor repository
- [X] Create unit tests
