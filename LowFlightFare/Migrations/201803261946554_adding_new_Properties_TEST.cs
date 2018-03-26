namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_new_Properties_TEST : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.search_results", "Airport_IATA_Code_ID", c => c.Int());
            CreateIndex("dbo.search_results", "Airport_IATA_Code_ID");
            AddForeignKey("dbo.search_results", "Airport_IATA_Code_ID", "dbo.airport_IATA_codes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.search_results", "Airport_IATA_Code_ID", "dbo.airport_IATA_codes");
            DropIndex("dbo.search_results", new[] { "Airport_IATA_Code_ID" });
            DropColumn("dbo.search_results", "Airport_IATA_Code_ID");
        }
    }
}
