namespace OfficeFiddleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fiddles", "Version", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fiddles", "Version");
        }
    }
}
