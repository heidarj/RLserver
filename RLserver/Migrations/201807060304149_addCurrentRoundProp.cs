namespace RLserver.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCurrentRoundProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "CurrentRound", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "CurrentRound");
        }
    }
}
