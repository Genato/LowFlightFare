namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_locale_and_settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        _Localization = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LocalizationID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locales", t => t.LocalizationID_ID)
                .Index(t => t.LocalizationID_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "LocalizationID_ID", "dbo.Locales");
            DropIndex("dbo.Settings", new[] { "LocalizationID_ID" });
            DropTable("dbo.Settings");
            DropTable("dbo.Locales");
        }
    }
}
