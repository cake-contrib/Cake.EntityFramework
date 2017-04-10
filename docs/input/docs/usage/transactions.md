---
Order: 25
Title: Run Migrations in Transactions
---

Run migrations in transactions. Keep in mind that this only works with supported providers (e.g. basiclly everyone except MySQL and Oracle). For example in PostgreSQL (Npgsql) you would do:

```cake
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