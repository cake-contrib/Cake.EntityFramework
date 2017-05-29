using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Age", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Students", "Age");
        }
    }
}