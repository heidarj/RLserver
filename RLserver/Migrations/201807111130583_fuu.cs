namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMatches", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamMatches", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.ApplicationUserTeams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamMatches", new[] { "Team_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Match_Id" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "Team_Id" });
            DropIndex("dbo.TournamentTeams", new[] { "Tournament_Id" });
            DropIndex("dbo.TournamentTeams", new[] { "Team_Id" });
            AddColumn("dbo.Teams", "Match_Id", c => c.Int());
            AddColumn("dbo.Teams", "Tournament_Id", c => c.Int());
            AddColumn("dbo.Teams", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teams", "Match_Id");
            CreateIndex("dbo.Teams", "Tournament_Id");
            CreateIndex("dbo.Teams", "ApplicationUser_Id");
            AddForeignKey("dbo.Teams", "Match_Id", "dbo.Matches", "Id");
            AddForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.TeamMatches");
            DropTable("dbo.ApplicationUserTeams");
            DropTable("dbo.TournamentTeams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TournamentTeams",
                c => new
                    {
                        Tournament_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_Id, t.Team_Id });
            
            CreateTable(
                "dbo.ApplicationUserTeams",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Team_Id });
            
            CreateTable(
                "dbo.TeamMatches",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Match_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Match_Id });
            
            DropForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Teams", "Match_Id", "dbo.Matches");
            DropIndex("dbo.Teams", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            DropIndex("dbo.Teams", new[] { "Match_Id" });
            DropColumn("dbo.Teams", "ApplicationUser_Id");
            DropColumn("dbo.Teams", "Tournament_Id");
            DropColumn("dbo.Teams", "Match_Id");
            CreateIndex("dbo.TournamentTeams", "Team_Id");
            CreateIndex("dbo.TournamentTeams", "Tournament_Id");
            CreateIndex("dbo.ApplicationUserTeams", "Team_Id");
            CreateIndex("dbo.ApplicationUserTeams", "ApplicationUser_Id");
            CreateIndex("dbo.TeamMatches", "Match_Id");
            CreateIndex("dbo.TeamMatches", "Team_Id");
            AddForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserTeams", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserTeams", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamMatches", "Match_Id", "dbo.Matches", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamMatches", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
