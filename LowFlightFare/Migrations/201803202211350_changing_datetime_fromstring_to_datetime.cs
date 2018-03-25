namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_datetime_fromstring_to_datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.search_parameters", "Depart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.search_parameters", "Return", c => c.DateTime(nullable: false));
            AlterColumn("dbo.search_results", "Depart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.search_results", "Return", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.search_results", "Return", c => c.String());
            AlterColumn("dbo.search_results", "Depart", c => c.String());
            AlterColumn("dbo.search_parameters", "Return", c => c.String());
            AlterColumn("dbo.search_parameters", "Depart", c => c.String());
        }
    }
}
