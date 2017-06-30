using System.Collections.Generic;

using Cake.EntityFramework.Models;

namespace Cake.EntityFramework.Interfaces
{
    /// <summary>
    /// Back-end migrator used in the MarshalByReference
    /// </summary>
    public interface IEfMigratorBackend
    {
        /// <summary>
        /// Gets a boolean value if the migration is currently ready.
        /// Specifically the AppDomain is ready
        /// </summary>
        bool Ready { get; }

        /// <summary>
        /// Gets a list of names of local migrations
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
        /// Gets the name of the current migration
        /// </summary>
        /// <returns>string name of latest migration</returns>
        string GetCurrentMigration();

        /// <summary>
        /// Determines if there are any pending migrations
        /// </summary>
        /// <returns>true if had migrations pending, otherwise false.</returns>
        bool HasPendingMigrations();

        /// <summary>
        /// Migrates the data store to the specific version
        /// </summary>
        /// <param name="version">Name of the migration to migrate to</param>
        /// <returns>Migration Result if the migration was successful in AppDomain</returns>
        MigrationResult MigrateTo(string version);

        /// <summary>
        /// Migrates the data store to the lastest version if any
        /// </summary>
        /// <returns>Migration Result if the migration was successful in AppDomain</returns>
        MigrationResult MigrateToLatest();

        /// <summary>
        /// Generates a script for the latest version if any
        /// </summary>
        /// <returns>ScriptResult with Migration Script</returns>
        ScriptResult GenerateScriptForLatest();

        /// <summary>
        /// Generates a script for the specific version if any
        /// </summary>
        /// <param name="version">Version to Generate a script for.</param>
        /// <returns>ScriptResult with Migration Script</returns>
        ScriptResult GenerateScriptForVersion(string version);

        /// <summary>
        /// Gets the name of the latest migration
        /// </summary>
        /// <returns>Latest migration name</returns>
        string GetLatestMigration();

        /// <summary>
        /// Runs the seed method on the target database.
        /// </summary>
        /// <returns></returns>
        void RunSeedMethod();
    }
}