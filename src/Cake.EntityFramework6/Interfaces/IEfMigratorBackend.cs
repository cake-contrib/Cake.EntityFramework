namespace Cake.EntityFramework6.Interfaces
{
    using System.Collections.Generic;

    using Cake.EntityFramework6.Models;

    public interface IEfMigratorBackend
    {
        bool Ready { get; }
        IEnumerable<string> GetLocalMigrations();
        IEnumerable<string> GetRemoteMigrations();
        IEnumerable<string> GetPendingMigrations();
        string GetCurrentMigration();
        bool HasPendingMigrations();
        MigrationResult MigrateTo(string version);
        MigrationResult MigrateToLatest();
        string GetLatestMigration();
    }
}