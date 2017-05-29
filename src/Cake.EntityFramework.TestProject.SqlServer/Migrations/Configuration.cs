using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
        }
    }
}