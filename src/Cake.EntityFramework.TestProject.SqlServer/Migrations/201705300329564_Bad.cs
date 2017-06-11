namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
