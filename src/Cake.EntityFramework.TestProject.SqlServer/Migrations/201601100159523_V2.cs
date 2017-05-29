using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "LastSchool", c => c.String());
            AddColumn("dbo.Students", "EyeColor", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Students", "EyeColor");
            DropColumn("dbo.Students", "LastSchool");
        }
    }
}