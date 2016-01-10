namespace Cake.EntityFramework.TestProject.Postgres.Actors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Sql;

    using Cake.EntityFramework.TestProject.Postgres.Models;

    using Npgsql;

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AtomicMigrationScriptBuilder : NpgsqlMigrationSqlGenerator
    {
        public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
        {
            yield return new MigrationStatement { Sql = "BEGIN TRANSACTION" };

            foreach (var migrationStatement in base.Generate(migrationOperations, providerManifestToken))
            {
                yield return migrationStatement;
            }

            yield return new MigrationStatement { Sql = "COMMIT TRANSACTION" };
        }
    }
}
