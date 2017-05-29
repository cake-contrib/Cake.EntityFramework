using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "MotherAge", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Students", "MotherAge");
        }
    }
}