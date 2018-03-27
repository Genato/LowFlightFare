namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamed_properties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.search_results", "Price", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.search_results", "Price", c => c.Double(nullable: false));
        }
    }
}
