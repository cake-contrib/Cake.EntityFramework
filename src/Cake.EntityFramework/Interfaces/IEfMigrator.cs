using System;
using System.Collections.Generic;

namespace Cake.EntityFramework.Interfaces
{
    /// <summary>
    /// Entity Framework Migrator 
    /// </summary>
    public interface IEfMigrator : IDisposable
    {
        /// <summary>
        /// Gets a boolean value if the migration was commited successfully
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// Gets a boolean value if the migration is currently ready
        /// </summary>
        bool Ready { get; }

        /// <summary>
        /// Commits the changes to the data store
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollsback any changes made to the data store
        /// </summary>
        void Rollback();

        /// <summary>
        /// Gets all migrations that are defined in the configured migrations assembly.
        /// </summary>
        /// <returns>List of strings</returns>        
        IEnumerable<string> GetLocalMigrations();

        /// <summary>
        /// Gets all migrations that have been applied to the target database.
        /// </summary>
        /// <returns>List of strings</returns>
        IEnumerable<string> GetRemoteMigrations();

        /// <summary>
        /// Gets all migrations that are defined in the assembly but haven't been applied to the target database.
        /// </summary>
        /// <returns>List of pending migrations if any</returns>
        IEnumerable<string> GetPendingMigrations();

        /// <summary>
        /// Determines if there are any pending migrations
        /// </summary>
        /// <returns>true if had migrations pending, otherwise false.</returns>
        bool HasPendingMigrations();
        
        /// <summary>
        /// Migrates the data store to the specific version
        /// </summary>
        /// <param name="version">Name of the migration to migrate to</param>
        /// <returns>true if migration was successful, otherwise false</returns>
        bool MigrateTo(string version);

        /// <summary>
        /// Migrates the data store to the lastest version if any
        /// </summary>
        /// <returns>true if migration was susccessful, otherwise false</returns>
        bool MigrateToLatest();

        /// <summary>
        ///  Gets last migration that has been applied to the target database.
        /// </summary>
        /// <returns>Name of the migration</returns>
        string GetCurrentMigration();

        /// <summary>
        /// Gets latest migration that is defined in the assembly but has not been applied to the target database.
        /// </summary>
        /// <returns>Latest migration name</returns>
        string GetLatestMigration();
    }
}