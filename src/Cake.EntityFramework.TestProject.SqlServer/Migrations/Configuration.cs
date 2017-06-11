using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        static Configuration()
        {
            Database.SetInitializer<SchoolContext>(new DropCreateDatabaseAlways<SchoolContext>());
        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
        }
    }
}