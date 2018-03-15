namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.airport_IATA_codes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IATA_code = c.String(),
                        AirportName = c.String(),
                        TownName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrencyCode = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.search_parameters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From_IATA_code = c.String(),
                        To_IATA_code = c.String(),
                        Depart = c.DateTime(nullable: false),
                        Return = c.DateTime(nullable: false),
                        PassangerNumber = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.search_results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From_IATA_code = c.String(),
                        To_IATA_code = c.String(),
                        Depart = c.DateTime(nullable: false),
                        Return = c.DateTime(nullable: false),
                        OutboundInterchanges = c.Int(nullable: false),
                        ReturnInterchanges = c.Int(nullable: false),
                        PassangerNumber = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.search_results");
            DropTable("dbo.search_parameters");
            DropTable("dbo.currencies");
            DropTable("dbo.airport_IATA_codes");
        }
    }
}
