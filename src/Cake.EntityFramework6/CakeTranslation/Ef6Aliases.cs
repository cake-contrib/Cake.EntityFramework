using System.Configuration;

namespace Cake.EntityFramework6.CakeTranslation
{
    using System;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.EntityFramework6.Interfaces;
using Cake.EntityFramework6.Migrator;

namespace Cake.EntityFramework6.CakeTranslation
{
    /// <summary>
    /// A set of Cake aliases for Entity Framework 6 code-first migrations.
    /// </summary>
    [CakeAliasCategory("EntityFramework6")]
    public static class Ef6Aliases
    {
        /// <summary>
        /// Creates a new EfMigrator for use in EF6 code-first migrations.
        /// </summary>
        /// <example>
        /// <code>
        /// using(var migrator = CreateEfMigrator(efMigratorSettings)) 
        /// {
        ///      migrator.MigrateToLatest();
        ///      migrator.Commit();
        /// }
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings <see cref="EfMigratorSettings"/>.</param>
        /// <returns>An EfMigrator <see cref="IEfMigrator"/>.</returns>
        [CakeMethodAlias]
        public static IEfMigrator CreateEfMigrator(this ICakeContext context, EfMigratorSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (settings.AssemblyPath == null)
            {
                throw new ArgumentNullException(nameof(settings.AssemblyPath));
            }

            if (settings.AppConfigPath == null)
            {
                throw new ArgumentNullException(nameof(settings.AppConfigPath));
            }

            if (settings.ConfigurationClass == null)
            {
                throw new ArgumentNullException(nameof(settings.ConfigurationClass));
            }

            if ((settings.ConnectionString == null || settings.ConnectionProvider == null) 
                && settings.ConnectionName != null)
            {
                string connectionString;
                string connectionProvider;
                SetConnectionStringFromAppConfig(settings, settings.ConnectionName, out connectionProvider, out connectionString);

                if (connectionString == null)
                    throw new ArgumentNullException(nameof(settings.ConnectionString));

                if (connectionProvider ==  null)
                    throw new ArgumentNullException(nameof(settings.ConnectionProvider));

                settings.ConnectionString = connectionString;
                settings.ConnectionProvider = connectionProvider;
            }

            if (settings.ConnectionProvider == null)
            {
                throw new ArgumentNullException(nameof(settings.ConnectionProvider));
            }

            if (settings.ConnectionString == null)
            {
               throw new ArgumentNullException(nameof(settings.ConnectionString));
            }

            return new EfMigrator(
                settings.AssemblyPath.FullPath,
                settings.ConfigurationClass,
                settings.AppConfigPath.FullPath,
                settings.ConnectionString,
                settings.ConnectionProvider,
                new CakeLogger(context.Log)
            );
        }

        private static void SetConnectionStringFromAppConfig(EfMigratorSettings settings, string connectionName, out string connectionProvider, out string connectionString)
        {
            var configMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = $@"{settings.AppConfigPath}"
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            var x = config.ConnectionStrings?.ConnectionStrings[connectionName];

            if (x == null)
                throw new Exception($"Unable to load configuration from path {settings.AppConfigPath}");

            connectionString = x?.ConnectionString;
            connectionProvider = x?.ProviderName;

            if (connectionString == null)
                throw new Exception("Unable to parse connection string from app config.");
        }
    }
}