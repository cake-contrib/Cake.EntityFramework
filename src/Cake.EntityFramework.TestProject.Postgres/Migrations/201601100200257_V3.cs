using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "Age", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("public.Students", "Age");
        }
    }
}