namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "MotherAge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Students", "MotherAge");
        }
    }
}
