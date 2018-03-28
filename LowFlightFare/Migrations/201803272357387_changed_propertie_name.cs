namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_propertie_name : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Settings", "LocalizationID_ID", "dbo.Locales");
            DropIndex("dbo.Settings", new[] { "LocalizationID_ID" });
            AddColumn("dbo.Settings", "LocalizationID", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "LocalizationID_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "LocalizationID_ID", c => c.Int());
            DropColumn("dbo.Settings", "LocalizationID");
            CreateIndex("dbo.Settings", "LocalizationID_ID");
            AddForeignKey("dbo.Settings", "LocalizationID_ID", "dbo.Locales", "ID");
        }
    }
}
