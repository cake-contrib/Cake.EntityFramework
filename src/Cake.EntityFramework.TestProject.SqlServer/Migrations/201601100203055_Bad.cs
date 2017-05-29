using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    public partial class Bad : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "BadColumn");
        }

        public override void Down()
        {
        }
    }
}