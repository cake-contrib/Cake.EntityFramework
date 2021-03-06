﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Models;
using System.Data.Entity.Migrations.Infrastructure;

namespace Cake.EntityFramework.Migrator
{
    public class EfMigratorBackend : MarshalByRefObject, IEfMigratorBackend
    {
        private string _parrentPath;
        private DbMigrator _dbMigrator;
        private MigratorScriptingDecorator _scripter;

        /// <summary>
        /// Gets a boolean value if the migration is currently ready.
        /// Specifically the AppDomain is ready
        /// </summary>
        public bool Ready { get; private set; }

        /// <summary>
        /// Gets all migrations that are defined in the configured migrations assembly.
        /// </summary>
        /// <returns>List of migrations</returns>  
        public IEnumerable<string> GetLocalMigrations()
        {
            AssertForReady();
            return _dbMigrator.GetLocalMigrations();
        }

        /// <summary>
        /// Gets all migrations that have been applied to the target database.
        /// </summary>
        /// <returns>List of migrations</returns>
        public IEnumerable<string> GetRemoteMigrations()
        {
            AssertForReady();
            return _dbMigrator.GetDatabaseMigrations();
        }

        /// <summary>
        /// Gets all migrations that are defined in the assembly but haven't been applied to the target database.
        /// </summary>
        /// <returns>List of pending migrations if any</returns>
        public IEnumerable<string> GetPendingMigrations()
        {
            AssertForReady();
            return _dbMigrator.GetPendingMigrations();
        }

        /// <summary>
        ///  Gets last migration that has been applied to the target database.
        /// </summary>
        /// <returns>Name of the migration</returns>
        public string GetCurrentMigration()
        {
            AssertForReady();
            return _dbMigrator.GetDatabaseMigrations().FirstOrDefault();
        }

        /// <summary>
        /// Gets latest migration that is defined in the assembly but has not been applied to the target database.
        /// </summary>
        /// <returns>Name of the migration</returns>
        public string GetLatestMigration()
        {
            return _dbMigrator.GetPendingMigrations().LastOrDefault();
        }

        /// <summary>
        /// Updates the database to the current database version and runs the seed method. This should result in no schema changes.
        /// </summary>
        public void RunSeedMethod()
        {
            var current = GetCurrentMigration();
            MigrateTo(current);
        }

        /// <summary>
        /// Determines if there are any pending migrations
        /// </summary>
        /// <returns>true if had migrations pending, otherwise false.</returns>
        public bool HasPendingMigrations()
        {
            AssertForReady();
            return GetPendingMigrations().Any();
        }

        /// <summary>
        /// Migrates the data store to the specific version
        /// </summary>
        /// <param name="version">Name of the migration to migrate to</param>
        /// <returns>true if migration was successful, otherwise false</returns>
        public MigrationResult MigrateTo(string version)
        {
            AssertForReady();
            try
            {
                _dbMigrator.Update(version);
                return new MigrationResult(true);
            }
            catch (Exception e)
            {
                return new MigrationResult(false, e);
            }
        }

        /// <summary>
        /// Migrates the data store to the lastest version if any
        /// </summary>
        /// <returns>true if migration was susccessful, otherwise false</returns>
        public MigrationResult MigrateToLatest()
        {
            AssertForReady();
            var last = GetLatestMigration();
            if (last != null)            
                return MigrateTo(last);            

            return new MigrationResult(true);
        }

        /// <summary>
        /// Determines whether to allow data loss on the migration
        /// </summary>
        /// <param name="allowDataLossOnMigrations">tru to allow data loss</param>
        public void SetAllowDataLossOnMigrations(bool allowDataLossOnMigrations)
        {
            _dbMigrator.Configuration.AutomaticMigrationDataLossAllowed = allowDataLossOnMigrations;
        }

        /// <summary>
        /// Initalized the migrator with requred settings
        /// </summary>
        /// <param name="assemblyPath">the full path of the assembly the contains the DbConfiguration Class</param>
        /// <param name="qualifiedDbConfigName">Full Qualified Name of the DbConfigurationClass that contains the migrations</param>
        /// <param name="connectionString">ConnectionStringName or full connection string</param>
        /// <param name="connectionProvider">Invaiant Name of the Ado.Net Connection Provider</param>
        public void Initialize(string assemblyPath, string qualifiedDbConfigName, string connectionString, string connectionProvider, string appConfigPath)
        {
            if (Ready)
                throw new EfMigrationException("EfMigrator is already initialize and cannot be re-initialize.");

            var assemblyLocation = Path.GetFullPath(assemblyPath);
            if (!File.Exists(assemblyLocation))
                throw new EfMigrationException($"The assemblyPath '{assemblyPath}' must exist, it currently doesn't.");

            _parrentPath = Path.GetDirectoryName(assemblyLocation);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;

            //DbConnectionInfo for Migrator
            var dbMigrationsConfiguration = LoadConfiguration(assemblyLocation, qualifiedDbConfigName);
            if (dbMigrationsConfiguration == null)
                throw new EfMigrationException($"The qualifiedDbConfigName {qualifiedDbConfigName} must exist within {assemblyPath} and implement type {nameof(DbMigrationsConfiguration)}. Make sure this class exists or that this class is a Migration Configuration.");

            //DbConnectionInfo for Migrator
            dbMigrationsConfiguration.TargetDatabase = new DbConnectionInfo(connectionString, connectionProvider); ;
            _dbMigrator = new DbMigrator(dbMigrationsConfiguration);

            //Script Db Migrators
            var scriptDbMigrationConfiguration = Activator.CreateInstance(dbMigrationsConfiguration.GetType()) as DbMigrationsConfiguration;
            scriptDbMigrationConfiguration.TargetDatabase = new DbConnectionInfo(connectionString, connectionProvider);
            
            _scripter = new MigratorScriptingDecorator(new DbMigrator(scriptDbMigrationConfiguration));            

            Ready = true;
        }

        private DbMigrationsConfiguration LoadConfiguration(string assemblyLocation, string qualifiedDbConfigName)
        {
            return Assembly.LoadFrom(assemblyLocation)
                           .DefinedTypes
                           .Where(x => typeof(DbMigrationsConfiguration).IsAssignableFrom(x))
                           .Where(x => !x.IsAbstract)
                           .Select(x => Activator.CreateInstance(x) as DbMigrationsConfiguration)
                           .Where(x => x != null)
                           .SingleOrDefault(x => x.GetType().FullName == qualifiedDbConfigName);
        }

        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyPath =
                new DirectoryInfo(_parrentPath)
                    .GetFiles("*.dll")
                    .FirstOrDefault(x => x.Name == $"{args.Name}.dll");

            return assemblyPath != null ? Assembly.LoadFrom(assemblyPath.FullName) : null;
        }

        private void AssertForReady()
        {
            if (!Ready)
                throw new Exception("Safety assert failed, improbable code reached. EfMigrator is not ready.");
        }

        /// <summary>
        /// Generates a script for the latest version if any
        /// </summary>
        /// <returns>ScriptResult with Migration Script</returns>
        public ScriptResult GenerateScriptForLatest()
        {
            AssertForReady();

            var script = _scripter.ScriptUpdate(null, null);
            return new ScriptResult(true, script);
        }

        /// <summary>
        /// Generates a script for the specific version if any
        /// </summary>
        /// <param name="version">Version to Generate a script for.</param>
        /// <returns>ScriptResult with Migration Script</returns>
        public ScriptResult GenerateScriptForVersion(string version)
        {
            AssertForReady();
            try
            {
                if (string.IsNullOrWhiteSpace(version))
                    version = null;

                var script = _scripter.ScriptUpdate(null, version);
                return new ScriptResult(true, script);
            }
            catch (Exception e)
            {
                return new ScriptResult(false, null, e);
            }
        }
    }
}