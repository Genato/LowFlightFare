namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class separated_inbound_from_outbound_properties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.search_results", "From_Outbound", c => c.String());
            AddColumn("dbo.search_results", "To_Outbound", c => c.String());
            AddColumn("dbo.search_results", "From_Inbound", c => c.String());
            AddColumn("dbo.search_results", "To_Inbound", c => c.String());
            DropColumn("dbo.search_results", "From");
            DropColumn("dbo.search_results", "To");
        }
        
        public override void Down()
        {
            AddColumn("dbo.search_results", "To", c => c.String());
            AddColumn("dbo.search_results", "From", c => c.String());
            DropColumn("dbo.search_results", "To_Inbound");
            DropColumn("dbo.search_results", "From_Inbound");
            DropColumn("dbo.search_results", "To_Outbound");
            DropColumn("dbo.search_results", "From_Outbound");
        }
    }
}
