namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameing_properties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.search_results", "Depart", c => c.String());
            AlterColumn("dbo.search_results", "Return", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.search_results", "Return", c => c.DateTime(nullable: false));
            AlterColumn("dbo.search_results", "Depart", c => c.DateTime(nullable: false));
        }
    }
}
