namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertFuu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Match_Id" });
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            DropIndex("dbo.Teams", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.TeamMatches",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Match_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Match_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.ApplicationUserTeams",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Team_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.TournamentTeams",
                c => new
                    {
                        Tournament_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_Id, t.Team_Id })
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Tournament_Id)
                .Index(t => t.Team_Id);
            
            DropColumn("dbo.Teams", "Match_Id");
            DropColumn("dbo.Teams", "Tournament_Id");
            DropColumn("dbo.Teams", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Teams", "Tournament_Id", c => c.Int());
            AddColumn("dbo.Teams", "Match_Id", c => c.Int());
            DropForeignKey("dbo.TournamentTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentTeams", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.ApplicationUserTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.ApplicationUserTeams", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMatches", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.TeamMatches", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TournamentTeams", new[] { "Team_Id" });
            DropIndex("dbo.TournamentTeams", new[] { "Tournament_Id" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "Team_Id" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Match_Id" });
            DropIndex("dbo.TeamMatches", new[] { "Team_Id" });
            DropTable("dbo.TournamentTeams");
            DropTable("dbo.ApplicationUserTeams");
            DropTable("dbo.TeamMatches");
            CreateIndex("dbo.Teams", "ApplicationUser_Id");
            CreateIndex("dbo.Teams", "Tournament_Id");
            CreateIndex("dbo.Teams", "Match_Id");
            AddForeignKey("dbo.Teams", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Teams", "Match_Id", "dbo.Matches", "Id");
        }
    }
}
