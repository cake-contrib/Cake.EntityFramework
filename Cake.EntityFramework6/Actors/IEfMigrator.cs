namespace Cake.EntityFramework6.Actors
{
    using System;
    using System.Collections.Generic;

    public interface IEfMigrator : IDisposable
    {
        bool Commited { get; }
        bool Ready { get; }
        void Commit();
        void Rollback();
        IEnumerable<string> GetLocalMigrations();
        IEnumerable<string> GetRemoteMigrations();
        IEnumerable<string> GetPendingMigrations();
        bool HasPendingMigrations();
        bool MigrateTo(string version);
        bool MigrateToLatest();
        string GetCurrentMigration();
        string GetLatestMigration();
    }
}