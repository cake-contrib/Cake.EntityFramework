using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using Cake.EntityFramework.TestProject.SqlServer.Models;
using System.Data.Entity.SqlServer;

namespace Cake.EntityFramework.TestProject.SqlServer
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AtomicMigrationScriptBuilder : SqlServerMigrationSqlGenerator
    {
        public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
        {
            yield return new MigrationStatement {Sql = "BEGIN TRANSACTION"};

            foreach (var migrationStatement in base.Generate(migrationOperations, providerManifestToken))
            {
                yield return migrationStatement;
            }

            yield return new MigrationStatement {Sql = "COMMIT TRANSACTION"};
        }
    }
}