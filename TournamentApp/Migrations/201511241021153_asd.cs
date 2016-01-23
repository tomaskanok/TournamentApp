namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Paid = c.Boolean(nullable: false),
                        GroupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        TournamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.UserId)
                .Index(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Registrations", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Registrations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Registrations", new[] { "TournamentId" });
            DropIndex("dbo.Registrations", new[] { "UserId" });
            DropIndex("dbo.Registrations", new[] { "GroupId" });
            DropTable("dbo.Registrations");
        }
    }
}
