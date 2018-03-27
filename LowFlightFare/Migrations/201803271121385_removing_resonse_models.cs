namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removing_resonse_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.fare", "price_per_adult_ID", "dbo.price_per_adult");
            DropForeignKey("dbo.fare", "restrictions_ID", "dbo.restrictions");
            DropForeignKey("dbo.flight", "booking_info_ID", "dbo.booking_info");
            DropForeignKey("dbo.flight", "destination_ID", "dbo.airport");
            DropForeignKey("dbo.flight", "origin_ID", "dbo.airport");
            DropForeignKey("dbo.flight", "OutboundInbound_ID", "dbo.outbound_inbound");
            DropForeignKey("dbo.itinerary", "inbound_ID", "dbo.outbound_inbound");
            DropForeignKey("dbo.itinerary", "outbound_ID", "dbo.outbound_inbound");
            DropForeignKey("dbo.result", "fare_ID", "dbo.fare");
            DropForeignKey("dbo.itinerary", "Result_ID", "dbo.result");
            DropForeignKey("dbo.result", "SearchResults_ID", "dbo.search_results");
            DropIndex("dbo.fare", new[] { "price_per_adult_ID" });
            DropIndex("dbo.fare", new[] { "restrictions_ID" });
            DropIndex("dbo.flight", new[] { "booking_info_ID" });
            DropIndex("dbo.flight", new[] { "destination_ID" });
            DropIndex("dbo.flight", new[] { "origin_ID" });
            DropIndex("dbo.flight", new[] { "OutboundInbound_ID" });
            DropIndex("dbo.itinerary", new[] { "inbound_ID" });
            DropIndex("dbo.itinerary", new[] { "outbound_ID" });
            DropIndex("dbo.itinerary", new[] { "Result_ID" });
            DropIndex("dbo.result", new[] { "fare_ID" });
            DropIndex("dbo.result", new[] { "SearchResults_ID" });
            AddColumn("dbo.search_results", "From", c => c.String());
            AddColumn("dbo.search_results", "To", c => c.String());
            AddColumn("dbo.search_results", "Depart", c => c.DateTime(nullable: false));
            AddColumn("dbo.search_results", "Return", c => c.DateTime(nullable: false));
            AddColumn("dbo.search_results", "OutboundInterchanges", c => c.Int(nullable: false));
            AddColumn("dbo.search_results", "ReturnInterchanges", c => c.Int(nullable: false));
            AddColumn("dbo.search_results", "PassangerNumber", c => c.Int(nullable: false));
            AddColumn("dbo.search_results", "Price", c => c.Double(nullable: false));
            DropTable("dbo.airport");
            DropTable("dbo.booking_info");
            DropTable("dbo.fare");
            DropTable("dbo.price_per_adult");
            DropTable("dbo.restrictions");
            DropTable("dbo.flight");
            DropTable("dbo.itinerary");
            DropTable("dbo.outbound_inbound");
            DropTable("dbo.result");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.result",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fare_ID = c.Int(),
                        SearchResults_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.outbound_inbound",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.itinerary",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        inbound_ID = c.Int(),
                        outbound_ID = c.Int(),
                        Result_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.flight",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        departs_at = c.String(),
                        arrives_at = c.String(),
                        marketing_airline = c.String(),
                        operating_airline = c.String(),
                        flight_number = c.String(),
                        aircraft = c.String(),
                        booking_info_ID = c.Int(),
                        destination_ID = c.Int(),
                        origin_ID = c.Int(),
                        OutboundInbound_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.restrictions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        refundable = c.Boolean(nullable: false),
                        change_penalties = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.price_per_adult",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        total_fare = c.String(),
                        tax = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.fare",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        total_price = c.String(),
                        price_per_adult_ID = c.Int(),
                        restrictions_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.booking_info",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        travel_class = c.String(),
                        booking_code = c.String(),
                        seats_remaining = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.airport",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        airport = c.String(),
                        terminal = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.search_results", "Price");
            DropColumn("dbo.search_results", "PassangerNumber");
            DropColumn("dbo.search_results", "ReturnInterchanges");
            DropColumn("dbo.search_results", "OutboundInterchanges");
            DropColumn("dbo.search_results", "Return");
            DropColumn("dbo.search_results", "Depart");
            DropColumn("dbo.search_results", "To");
            DropColumn("dbo.search_results", "From");
            CreateIndex("dbo.result", "SearchResults_ID");
            CreateIndex("dbo.result", "fare_ID");
            CreateIndex("dbo.itinerary", "Result_ID");
            CreateIndex("dbo.itinerary", "outbound_ID");
            CreateIndex("dbo.itinerary", "inbound_ID");
            CreateIndex("dbo.flight", "OutboundInbound_ID");
            CreateIndex("dbo.flight", "origin_ID");
            CreateIndex("dbo.flight", "destination_ID");
            CreateIndex("dbo.flight", "booking_info_ID");
            CreateIndex("dbo.fare", "restrictions_ID");
            CreateIndex("dbo.fare", "price_per_adult_ID");
            AddForeignKey("dbo.result", "SearchResults_ID", "dbo.search_results", "ID");
            AddForeignKey("dbo.itinerary", "Result_ID", "dbo.result", "ID");
            AddForeignKey("dbo.result", "fare_ID", "dbo.fare", "ID");
            AddForeignKey("dbo.itinerary", "outbound_ID", "dbo.outbound_inbound", "ID");
            AddForeignKey("dbo.itinerary", "inbound_ID", "dbo.outbound_inbound", "ID");
            AddForeignKey("dbo.flight", "OutboundInbound_ID", "dbo.outbound_inbound", "ID");
            AddForeignKey("dbo.flight", "origin_ID", "dbo.airport", "ID");
            AddForeignKey("dbo.flight", "destination_ID", "dbo.airport", "ID");
            AddForeignKey("dbo.flight", "booking_info_ID", "dbo.booking_info", "ID");
            AddForeignKey("dbo.fare", "restrictions_ID", "dbo.restrictions", "ID");
            AddForeignKey("dbo.fare", "price_per_adult_ID", "dbo.price_per_adult", "ID");
        }
    }
}
