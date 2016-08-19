namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Students", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Students", "Age");
        }
    }
}
