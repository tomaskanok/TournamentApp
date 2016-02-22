namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFightTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(),
                        TimeEnd = c.DateTime(nullable: false),
                        Overtime = c.Boolean(nullable: false),
                        WayOfWin = c.String(),
                        PointsFirst = c.Int(nullable: false),
                        PointsSecond = c.Int(nullable: false),
                        Referee = c.String(maxLength: 128),
                        FighterFirst = c.String(maxLength: 128),
                        FighterSecond = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FighterFirst)
                .ForeignKey("dbo.AspNetUsers", t => t.FighterSecond)
                .ForeignKey("dbo.AspNetUsers", t => t.Referee)
                .Index(t => t.Referee)
                .Index(t => t.FighterFirst)
                .Index(t => t.FighterSecond);       
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        Web = c.String(),
                        Prize = c.Int(nullable: false),
                        Info = c.String(),
                        StartReg = c.DateTime(nullable: false),
                        EndReg = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OrganizerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
           
            DropForeignKey("dbo.Fights", "Referee", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fights", "FighterSecond", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fights", "FighterFirst", "dbo.AspNetUsers");
            DropIndex("dbo.Fights", new[] { "FighterSecond" });
            DropIndex("dbo.Fights", new[] { "FighterFirst" });
            DropIndex("dbo.Fights", new[] { "Referee" });
            DropTable("dbo.Fights");
        }
    }
}
