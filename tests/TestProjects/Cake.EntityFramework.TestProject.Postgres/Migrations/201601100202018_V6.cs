namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "OtherThing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Students", "OtherThing");
        }
    }
}
