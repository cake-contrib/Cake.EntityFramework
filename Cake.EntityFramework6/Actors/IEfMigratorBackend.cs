namespace Cake.EntityFramework6.Actors
{
    using System.Collections.Generic;

    public interface IEfMigratorBackend
    {
        bool Ready { get; }
        IEnumerable<string> GetLocalMigrations();
        IEnumerable<string> GetRemoteMigrations();
        IEnumerable<string> GetPendingMigrations();
        string GetCurrentMigration();
        bool HasPendingMigrations();
        bool MigrateTo(string version);
        bool MigrateToLatest();
        string GetLatestMigration();
    }
}