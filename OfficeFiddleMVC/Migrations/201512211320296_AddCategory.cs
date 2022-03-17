namespace OfficeFiddleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Fiddles", "Description", c => c.String());
            AddColumn("dbo.Fiddles", "CategoryID_id", c => c.Int());
            CreateIndex("dbo.Fiddles", "CategoryID_id");
            AddForeignKey("dbo.Fiddles", "CategoryID_id", "dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fiddles", "CategoryID_id", "dbo.Categories");
            DropIndex("dbo.Fiddles", new[] { "CategoryID_id" });
            DropColumn("dbo.Fiddles", "CategoryID_id");
            DropColumn("dbo.Fiddles", "Description");
            DropTable("dbo.Categories");
        }
    }
}
