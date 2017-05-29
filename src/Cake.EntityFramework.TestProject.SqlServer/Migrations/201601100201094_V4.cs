using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "FatherAge", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Students", "FatherAge");
        }
    }
}