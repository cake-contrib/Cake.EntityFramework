namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "MothersAge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "MothersAge");
        }
    }
}
