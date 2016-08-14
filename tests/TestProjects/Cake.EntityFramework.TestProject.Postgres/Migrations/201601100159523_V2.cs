namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "LastSchool", c => c.String());
            AddColumn("public.Students", "EyeColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Students", "EyeColor");
            DropColumn("public.Students", "LastSchool");
        }
    }
}
