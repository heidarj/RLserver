namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeamApplicationUsers", newName: "ApplicationUserTeams");
            DropPrimaryKey("dbo.ApplicationUserTeams");
            AddPrimaryKey("dbo.ApplicationUserTeams", new[] { "ApplicationUser_Id", "Team_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserTeams");
            AddPrimaryKey("dbo.ApplicationUserTeams", new[] { "Team_Id", "ApplicationUser_Id" });
            RenameTable(name: "dbo.ApplicationUserTeams", newName: "TeamApplicationUsers");
        }
    }
}
