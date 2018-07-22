namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchShouldOnlyHaveOneTournament : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tournaments", "Match_Id", "dbo.Matches");
            DropIndex("dbo.Tournaments", new[] { "Match_Id" });
            AddColumn("dbo.Matches", "Tournament_Id", c => c.Int());
            AddColumn("dbo.Teams", "Description", c => c.String());
            AddColumn("dbo.Teams", "Slogan", c => c.String());
            AddColumn("dbo.Teams", "Logo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            AddColumn("dbo.AspNetUsers", "Slogan", c => c.String());
            AddColumn("dbo.AspNetUsers", "FacebookProfileUri", c => c.String());
            CreateIndex("dbo.Matches", "Tournament_Id");
            AddForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments", "Id");
            DropColumn("dbo.Tournaments", "Match_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "Match_Id", c => c.Int());
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropColumn("dbo.AspNetUsers", "FacebookProfileUri");
            DropColumn("dbo.AspNetUsers", "Slogan");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.Teams", "Logo");
            DropColumn("dbo.Teams", "Slogan");
            DropColumn("dbo.Teams", "Description");
            DropColumn("dbo.Matches", "Tournament_Id");
            CreateIndex("dbo.Tournaments", "Match_Id");
            AddForeignKey("dbo.Tournaments", "Match_Id", "dbo.Matches", "Id");
        }
    }
}
