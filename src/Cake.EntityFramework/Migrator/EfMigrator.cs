using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework.Migrator
{
    /// <summary>
    /// EntityFramework Migrator implentations
    /// </summary>
    public class EfMigrator : IEfMigrator
    {
        private readonly ILogger _logger;
        private AppDomain _domain;
        private readonly IEfMigratorBackend _migratorBackend;

        private const string InitialDatabase = "0";

        /// <summary>
        /// Gets a boolean value if the migration was commited successfully
        /// </summary>
        public bool Committed { get; private set; }

        /// <summary>
        /// Gets the name of the current migration
        /// </summary>
        public string CurrentMigration { get; private set; }

        /// <summary>
        /// Entity Framework Migration
        /// </summary>
        /// <param name="assemblyPath">full path to the assembly</param>
        /// <param name="qualifiedDbConfigName">Name of the DbConfiguration class to use for the migrations</param>
        /// <param name="appConfigPath">App.Config or Web.config file path</param>
        /// <param name="connectionString">Connectionsting name of actually connection string</param>
        /// <param name="connectionProvider">Name of the connection string provider</param>
        /// <param name="logger">Logger to write items to the console</param>
        /// <param name="allowDataLossOnMigrations">Determines whether to allow dataloss during the migration</param>
        public EfMigrator(string assemblyPath, string qualifiedDbConfigName, string appConfigPath, string connectionString, string connectionProvider,
                          ILogger logger, bool allowDataLossOnMigrations)
        {
            _logger = logger;

            _logger.Information($"Connection string being used is: {connectionString}"); 
            
            appConfigPath = Path.GetFullPath(appConfigPath);
            if (!File.Exists(appConfigPath))
            {
                throw new EfMigrationException($"The {nameof(appConfigPath)} '{appConfigPath}' must exist.");
            }

            var domainSetup = AppDomain.CurrentDomain.SetupInformation;
            domainSetup.ConfigurationFile = appConfigPath;
            _logger.Debug($"Prepared AppDomain setup using {appConfigPath} as the appconfig.");

            var domainName = $"EfMigrator:{Guid.NewGuid()}";
            _domain = AppDomain.CreateDomain(domainName, null, domainSetup);
            _logger.Debug($"Created new AppDomain named {domainName}.");

            var type = typeof(EfMigratorBackend);

            var fullPath = Assembly.GetAssembly(typeof(EfMigratorBackend)).Location;
            
            Debug.Assert(fullPath != null, "fullPath != null");

            var migrator = (EfMigratorBackend) _domain.CreateInstanceFromAndUnwrap(fullPath, type.FullName);
            _logger.Debug("Created new instance.");
            migrator.Initialize(assemblyPath, qualifiedDbConfigName, connectionString, connectionProvider, appConfigPath);

            _logger.Debug($"Initialized new {nameof(EfMigratorBackend)} within {domainName}.");

            CurrentMigration = migrator.GetCurrentMigration() ?? InitialDatabase;
            var currentMigrationStr = CurrentMigration == InitialDatabase ? "$InitialDatabase" : CurrentMigration;
            _logger.Information($"Current Migration is {currentMigrationStr}.");

            migrator.SetAllowDataLossOnMigrations(allowDataLossOnMigrations);

            _migratorBackend = migrator;
        }

        /// <summary>
        /// Entity Framework Migration
        /// </summary>
        /// <param name="migratorBackend">Ef Migrator Backend commnication used in AppDomain</param>
        /// <param name="logger">Logger to write items to the console</param>
        public EfMigrator(IEfMigratorBackend migratorBackend, ILogger logger)
        {
            _logger = logger;
            _migratorBackend = migratorBackend;
        }

        /// <summary>
        /// Gets a boolean value if the migration is currently ready
        /// </summary>
        public bool Ready => _migratorBackend != null && _migratorBackend.Ready;

        /// <summary>
        /// Commits the changes to the data store
        /// </summary>
        public void Commit()
        {
            Committed = true;
            CurrentMigration = GetCurrentMigration();
            _logger.Information($"Migrations are now commited at {CurrentMigration}.");
        }

        /// <summary>
        /// Rollsback any changes made to the data store
        /// </summary>
        public void Rollback()
        {
            if (Committed)            
                throw new Exception("Can't rollback when the migrations have been commited.");            

            if (CurrentMigration == null)
            {
                _logger.Error("Unknown last migration level, rollback not possible.");
                return;
            }

            _logger.Information($"Rollingback to {CurrentMigration}.");
            _migratorBackend.MigrateTo(CurrentMigration);
            _logger.Information("Migrations have been rolledbacked.");
        }

        /// <summary>
        /// Gets all migrations that are defined in the configured migrations assembly.
        /// </summary>
        /// <returns>List of migrations</returns>     
        public IEnumerable<string> GetLocalMigrations()
        {
            return _migratorBackend.GetLocalMigrations();
        }

        /// <summary>
        /// Gets all migrations that have been applied to the target database.
        /// </summary>
        /// <returns>List of strings</returns>
        public IEnumerable<string> GetRemoteMigrations()
        {
            return _migratorBackend.GetRemoteMigrations();
        }

        /// <summary>
        /// Gets all migrations that are defined in the assembly but haven't been applied to the target database.
        /// </summary>
        /// <returns>List of pending migrations if any</returns>
        public IEnumerable<string> GetPendingMigrations()
        {
            return _migratorBackend.GetPendingMigrations();
        }

        /// <summary>
        /// Gets the name of the current migration
        /// </summary>
        /// <returns>string name of latest migration</returns>
        public string GetCurrentMigration()
        {
            return _migratorBackend.GetCurrentMigration();
        }

        /// <summary>
        /// Determines if there are any pending migrations
        /// </summary>
        /// <returns>true if had migrations pending, otherwise false.</returns>
        public bool HasPendingMigrations()
        {
            return _migratorBackend.HasPendingMigrations();
        }

        /// <summary>
        /// Migrates the data store to the specific version
        /// </summary>
        /// <param name="version">Name of the migration to migrate to</param>
        /// <returns>true if migration was successful, otherwise false</returns>
        public bool MigrateTo(string version)
        {
            var result = _migratorBackend.MigrateTo(version);
            if (!result.IsSuccess)
            {
                throw new Exception($"Error when migrating: {result.Exception.Message}.", result.Exception); 
            }
            return result.IsSuccess;
        }

        /// <summary>
        /// Migrates the data store to the lastest version if any
        /// </summary>
        /// <returns>true if migration was susccessful, otherwise false</returns>
        public bool MigrateToLatest()
        {
            var result = _migratorBackend.MigrateToLatest();
            if (!result.IsSuccess)
            {
                throw new Exception($"Error when migrating: {result.Exception.Message}.", result.Exception);
            }
            return result.IsSuccess;
        }

        /// <summary>
        /// Gets the name of the latest migration
        /// </summary>
        /// <returns>Latest migration name</returns>
        public string GetLatestMigration()
        {
            return _migratorBackend.GetLatestMigration();
        }

        /// <summary>
        /// Disposes of any resources and unloads te temp AppDomain used for the migrations
        /// </summary>
        public void Dispose()
        {
            if (!Committed)
            {
                _logger.Warning("Nothing commited, rollingback.");
                Rollback();
            }

            if (_domain != null)
            {
                AppDomain.Unload(_domain);
                _logger.Debug("AppDomain has been unloaded.");

                _domain = null;
            }
        }

        /// <summary>
        /// Generates a script from the data store to the specific version
        /// </summary>
        /// <param name="version">Name of the migration to generate a script to</param>
        /// <returns>true if migration was successful, otherwise false</returns>
        public string GenerateScriptForVersion(string version)
        {
            var result = _migratorBackend.GenerateScriptForVersion(version);

            if (!result.IsSuccess)           
                throw new Exception($"Error when generating a script: {result.Exception.Message}.", result.Exception);
            
            return result.Script;
        }

        /// <summary>
        /// Generates a script from the data store for the latest version
        /// </summary>
        /// <returns>true if script generation was successful, otherwise false</returns>
        public string GenerateScriptForLatest()
        {
            var result = _migratorBackend.GenerateScriptForLatest();

            if (!result.IsSuccess)            
                throw new Exception($"Error when generating a migration script: {result.Exception.Message}.", result.Exception);
            
            return result.Script;
        }

        /// <summary>
        /// Runs the Seed Method on the targeted database.
        /// </summary>
        public void RunSeedMethod()
        {            
            _migratorBackend.RunSeedMethod();
        }
    }
}