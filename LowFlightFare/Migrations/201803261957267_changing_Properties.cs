namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_Properties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.search_results", "Airport_IATA_Code_ID", "dbo.airport_IATA_codes");
            DropIndex("dbo.search_results", new[] { "Airport_IATA_Code_ID" });
            AddColumn("dbo.search_results", "From", c => c.String());
            AddColumn("dbo.search_results", "To", c => c.String());
            DropColumn("dbo.search_results", "From_IATA_code");
            DropColumn("dbo.search_results", "To_IATA_code");
            DropColumn("dbo.search_results", "Airport_IATA_Code_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.search_results", "Airport_IATA_Code_ID", c => c.Int());
            AddColumn("dbo.search_results", "To_IATA_code", c => c.String());
            AddColumn("dbo.search_results", "From_IATA_code", c => c.String());
            DropColumn("dbo.search_results", "To");
            DropColumn("dbo.search_results", "From");
            CreateIndex("dbo.search_results", "Airport_IATA_Code_ID");
            AddForeignKey("dbo.search_results", "Airport_IATA_Code_ID", "dbo.airport_IATA_codes", "ID");
        }
    }
}
