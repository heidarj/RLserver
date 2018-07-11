namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTournamentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Tournaments", "Description", c => c.String());
            AddColumn("dbo.Tournaments", "TotalRounds", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "TotalRounds");
            DropColumn("dbo.Tournaments", "Description");
            DropColumn("dbo.Tournaments", "Name");
        }
    }
}
