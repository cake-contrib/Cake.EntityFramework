using Cake.Core.IO;

namespace Cake.EntityFramework6.CakeTranslation
{
    /// <summary>
    /// Contains settings for working with EntityFramework6 <see cref="Ef6Aliases"/>.
    /// </summary>
    public class EfMigratorSettings
    {
        /// <summary>
        /// Gets or sets the assembly path.
        /// </summary>
        /// <value>
        /// The path to the assembly where the EF configuration class is located.
        /// </value>
        public DirectoryPath AssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets the configuration class.
        /// </summary>
        /// <value>
        /// The fully qualified name of the EF configuration class for code-first migrations.
        /// </value>
        public string ConfigurationClass { get; set; }

        /// <summary>
        /// Gets or sets the app.config path.
        /// </summary>
        /// <value>
        /// The path to the app.config (or web.config) containing the settings/configurations used by EF.
        /// </value>
        public DirectoryPath AppConfigPath { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string to be used for code-first migrations.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the connection provider.
        /// </summary>
        /// <value>
        /// The connection provider to be used for code-first migrations.
        /// </value>
        public string ConnectionProvider { get; set; }

        /// <summary>
        /// The name of the connection string in the configuration file.
        /// This can be used to load the connection string and provider.
        /// </summary>
        public string ConnectionName { get; set; }

        /// Gets or sets a flag to allow data to be lost on a migration.
        /// This is the same as the '-force' flag when running migrations through visual studio.
        /// </summary>
        public bool AllowDataLossOnMigrations { get; set; }
    }
}