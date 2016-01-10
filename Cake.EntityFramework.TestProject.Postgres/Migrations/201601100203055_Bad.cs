namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bad : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.Students", "BadColumn");
        }
        
        public override void Down()
        {
        }
    }
}
