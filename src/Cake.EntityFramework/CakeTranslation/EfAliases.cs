using System.Configuration;

namespace Cake.EntityFramework.CakeTranslation
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.EntityFramework.Interfaces;
    using Cake.EntityFramework.Migrator;

    /// <summary>
    /// A set of Cake aliases for Entity Framework (not .NET Core) code-first migrations.
    /// </summary>
    [CakeAliasCategory("EntityFramework")]
    public static class EfAliases
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

            if (settings.ConnectionString == null && settings.ConnectionProvider == null
                && settings.ConnectionName != null)
            {
                string connectionString;
                string connectionProvider;
                SetConnectionStringFromAppConfig(settings, out connectionProvider, out connectionString);

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
                new CakeLogger(context.Log),
                settings.AllowDataLossOnMigrations
                );
        }

        /// <summary>
        /// Retrieves the connection string & connection provider from application config,
        /// if the app config path is set & there is a connection name.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="connectionProvider"></param>
        /// <param name="connectionString"></param>
        private static void SetConnectionStringFromAppConfig(EfMigratorSettings settings, out string connectionProvider, out string connectionString)
        {
            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = $@"{settings.AppConfigPath}" };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            var connectionStringSettings = config.ConnectionStrings?.ConnectionStrings[settings.ConnectionName];

            if (connectionStringSettings == null)
                throw new ArgumentException($"Unable to load connection information for connection name {settings.ConnectionName}");

            connectionString = connectionStringSettings.ConnectionString;
            connectionProvider = connectionStringSettings.ProviderName;

            if (connectionString == null)
                throw new InvalidOperationException("Unable to get connection string from app config.");

            if (connectionProvider == null)
                throw new InvalidOperationException("Unable to get connection provider from app config.");
        }
    }
}