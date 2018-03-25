namespace LowFlightFare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_new_Properties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.search_results", "SearchParametersID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.search_results", "SearchParametersID");
        }
    }
}
