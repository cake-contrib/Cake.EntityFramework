using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "MotherAge", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("public.Students", "MotherAge");
        }
    }
}