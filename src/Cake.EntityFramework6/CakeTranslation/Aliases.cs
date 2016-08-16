namespace Cake.EntityFramework6.CakeTranslation
{
    using System;

    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.EntityFramework6.Interfaces;
    using Cake.EntityFramework6.Migrator;

    public static class Aliases
    {
        [CakeMethodAlias]
        public static IEfMigrator CreateEfMigrator(this ICakeContext context, EfMigratorSettings settings)
        {
            if (settings.AssemblyPath == null)
            {
                throw new ArgumentException("AssemblyPath must have value.", nameof(settings));
            }

            if (settings.AppConfigPath == null)
            {
                throw new ArgumentException("AppConfigPath must have value.", nameof(settings));
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
    }
}
