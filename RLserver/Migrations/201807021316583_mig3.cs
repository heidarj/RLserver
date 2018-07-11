namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MatchTeams", newName: "TeamMatches");
            DropPrimaryKey("dbo.TeamMatches");
            AddPrimaryKey("dbo.TeamMatches", new[] { "Team_Id", "Match_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TeamMatches");
            AddPrimaryKey("dbo.TeamMatches", new[] { "Match_Id", "Team_Id" });
            RenameTable(name: "dbo.TeamMatches", newName: "MatchTeams");
        }
    }
}
