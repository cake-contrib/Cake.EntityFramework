namespace Cake.EntityFramework6.Actors
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Cake.EntityFramework6.Interfaces;

    public class EfMigrator : IEfMigrator
    {
        private readonly ILogger _logger;
        private AppDomain _domain;
        private readonly IEfMigratorBackend _migratorBackend;

        public bool Commited { get; private set; }
        public string CurrentMigration { get; private set; }

        public EfMigrator(string assemblyPath, string qualifiedDbConfigName, string appConfigPath, string connectionString, string connectionProvider, ILogger logger)
        {
            _logger = logger;

            appConfigPath = Path.GetFullPath(appConfigPath);
            if (!File.Exists(appConfigPath))
            {
                throw new Exception($"The appConfigPath '{appConfigPath}' must exist.");
            }

            var domainSetup = AppDomain.CurrentDomain.SetupInformation;
            domainSetup.ConfigurationFile = appConfigPath;
            _logger.Debug($"Prepared AppDomain setup using {appConfigPath} as the appconfig.");

            var domainName = $"EfMigrator:{Guid.NewGuid()}";
            _domain = AppDomain.CreateDomain(domainName, null, domainSetup);
            _logger.Debug($"Created new AppDomain named {domainName}.");

            var type = typeof(EfMigratorBackend);
            var migrator = (EfMigratorBackend) _domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            migrator.Initialize(assemblyPath, qualifiedDbConfigName, connectionString, connectionProvider);

            _logger.Debug($"Initialized new {nameof(EfMigratorBackend)} within AppDomain.");

            CurrentMigration = migrator.GetCurrentMigration();
            _logger.Information($"Current Migration is {CurrentMigration}.");

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
            Commited = true;
            CurrentMigration = GetCurrentMigration();
            _logger.Information($"Migrations are now commited at {CurrentMigration}.");
        }

        public void Rollback()
        {
            if (Commited)
            {
                throw new Exception("Can't rollback when the migrations have been commited.");
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
            return _migratorBackend.MigrateTo(version);
        }

        public bool MigrateToLatest()
        {
            return _migratorBackend.MigrateToLatest();
        }

        public string GetLatestMigration()
        {
            return _migratorBackend.GetLatestMigration();
        }

        public void Dispose()
        {
            if (!Commited)
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
