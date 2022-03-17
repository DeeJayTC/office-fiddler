namespace OfficeFiddleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fiddles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HTML = c.String(),
                        CSS = c.String(),
                        JS = c.String(),
                        userid = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Application = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fiddles");
        }
    }
}
