namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(),
                        TimeEnd = c.DateTime(),
                        Overtime = c.Boolean(),
                        WayOfWin = c.String(),
                        PointsFirst = c.Int(),
                        PointsSecond = c.Int(),
                        Referee = c.String(maxLength: 128),
                        FighterFirst = c.String(maxLength: 128),
                        FighterSecond = c.String(maxLength: 128),
                        RootFightId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FighterFirst)
                .ForeignKey("dbo.AspNetUsers", t => t.FighterSecond)
                .ForeignKey("dbo.AspNetUsers", t => t.Referee)
                .ForeignKey("dbo.Fights", t => t.RootFightId)
                .Index(t => t.Referee)
                .Index(t => t.FighterFirst)
                .Index(t => t.FighterSecond)
                .Index(t => t.RootFightId);
            
            AddColumn("dbo.Groups", "ClosedRegistation", c => c.Boolean());
            AddColumn("dbo.Groups", "IdFinalFight", c => c.Int());
            CreateIndex("dbo.Groups", "IdFinalFight");
            AddForeignKey("dbo.Groups", "IdFinalFight", "dbo.Fights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "IdFinalFight", "dbo.Fights");
            DropForeignKey("dbo.Fights", "RootFightId", "dbo.Fights");
            DropForeignKey("dbo.Fights", "Referee", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fights", "FighterSecond", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fights", "FighterFirst", "dbo.AspNetUsers");
            DropIndex("dbo.Fights", new[] { "RootFightId" });
            DropIndex("dbo.Fights", new[] { "FighterSecond" });
            DropIndex("dbo.Fights", new[] { "FighterFirst" });
            DropIndex("dbo.Fights", new[] { "Referee" });
            DropIndex("dbo.Groups", new[] { "IdFinalFight" });
            DropColumn("dbo.Groups", "IdFinalFight");
            DropColumn("dbo.Groups", "ClosedRegistation");
            DropTable("dbo.Fights");
        }
    }
}
