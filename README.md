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

Example: [Link](tests/TestProjects/Cake.EntityFramework.TestProject.Postgres/build.cake)

```cake
#addin "Cake.EntityFramework6"

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

App configurations are essentially read at startup and some values are even read-only, hence support for dynamic Entity Framework providers is difficult. Since Cake scripts are essentially just dotNet programs, the same issues arise. 

So to support multiple EF providers and configurations some ~~hackish~~ clever programming is required. This project uses a technique called Cross-AppDomain-Remoting which constructs the EF migrator in another app domain. By doing this we can customize which app config is read at startup among other things (which allows us to have the DbProviderFactories dynamically generated). 

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
