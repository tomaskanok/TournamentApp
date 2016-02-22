namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RootForFightTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fights", "RootFightId", c => c.Int());
            CreateIndex("dbo.Fights", "RootFightId");
            AddForeignKey("dbo.Fights", "RootFightId", "dbo.Fights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fights", "RootFightId", "dbo.Fights");
            DropIndex("dbo.Fights", new[] { "RootFightId" });
            DropColumn("dbo.Fights", "RootFightId");
        }
    }
}
