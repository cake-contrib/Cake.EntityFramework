using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Cake.EntityFramework.Migrator
{
    public class EfMigrator : IEfMigrator
    {
        private readonly ILogger _logger;
        private AppDomain _domain;
        private readonly IEfMigratorBackend _migratorBackend;

        private const string InitialDatabase = "0";

        public bool Committed { get; private set; }

        public string CurrentMigration { get; private set; }

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
            migrator.Initialize(assemblyPath, qualifiedDbConfigName, connectionString, connectionProvider);

            _logger.Debug($"Initialized new {nameof(EfMigratorBackend)} within {domainName}.");

            CurrentMigration = migrator.GetCurrentMigration() ?? InitialDatabase;
            var currentMigrationStr = CurrentMigration == InitialDatabase ? "$InitialDatabase" : CurrentMigration;
            _logger.Information($"Current Migration is {currentMigrationStr}.");

            migrator.SetAllowDataLossOnMigrations(allowDataLossOnMigrations);

            _migratorBackend = migrator;
        }

        public EfMigrator(IEfMigratorBackend migratorBackend, ILogger logger)
        {
            _logger = logger;
            _migratorBackend = migratorBackend;
        }

        public bool Ready => _migratorBackend != null && _migratorBackend.Ready;

        public void Commit()
        {
            Committed = true;
            CurrentMigration = GetCurrentMigration();
            _logger.Information($"Migrations are now commited at {CurrentMigration}.");
        }

        public void Rollback()
        {
            if (Committed)
            {
                throw new Exception("Can't rollback when the migrations have been commited.");
            }

            if (CurrentMigration == null)
            {
                _logger.Error("Unknown last migration level, rollback not possible.");
                return;
            }

            _logger.Information($"Rollingback to {CurrentMigration}.");
            _migratorBackend.MigrateTo(CurrentMigration);
            _logger.Information("Migrations have been rolledbacked.");
        }

        public IEnumerable<string> GetLocalMigrations()
        {
            return _migratorBackend.GetLocalMigrations();
        }

        public IEnumerable<string> GetRemoteMigrations()
        {
            return _migratorBackend.GetRemoteMigrations();
        }

        public IEnumerable<string> GetPendingMigrations()
        {
            return _migratorBackend.GetPendingMigrations();
        }

        public string GetCurrentMigration()
        {
            return _migratorBackend.GetCurrentMigration();
        }

        public bool HasPendingMigrations()
        {
            return _migratorBackend.HasPendingMigrations();
        }

        public bool MigrateTo(string version)
        {
            var result = _migratorBackend.MigrateTo(version);
            if (!result.IsSuccess)
            {
                throw new Exception($"Error when migrating: {result.Exception.Message}.", result.Exception); 
            }
            return result.IsSuccess;
        }

        public bool MigrateToLatest()
        {
            var result = _migratorBackend.MigrateToLatest();
            if (!result.IsSuccess)
            {
                throw new Exception($"Error when migrating: {result.Exception.Message}.", result.Exception);
            }
            return result.IsSuccess;
        }

        public string GetLatestMigration()
        {
            return _migratorBackend.GetLatestMigration();
        }

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
    }
}