using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "FatherAge", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("public.Students", "FatherAge");
        }
    }
}