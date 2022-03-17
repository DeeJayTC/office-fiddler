namespace OfficeFiddleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryEdit : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Fiddles", name: "CategoryID_id", newName: "Category_id");
            RenameIndex(table: "dbo.Fiddles", name: "IX_CategoryID_id", newName: "IX_Category_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Fiddles", name: "IX_Category_id", newName: "IX_CategoryID_id");
            RenameColumn(table: "dbo.Fiddles", name: "Category_id", newName: "CategoryID_id");
        }
    }
}
