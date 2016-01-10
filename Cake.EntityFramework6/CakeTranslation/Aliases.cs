namespace Cake.EntityFramework6.CakeTranslation
{
    using System;
    using System.Runtime.InteropServices;

    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.Diagnostics;
    using Cake.EntityFramework6.Actors;
    using Cake.EntityFramework6.Models;

    public static class Aliases
    {
        [CakeMethodAlias]
        public static IEfMigrator CreateEfMigrator(this ICakeContext context, EfMigratorSettings settings)
        {
            ICakeLog a = context.Log;

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
