namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditGroupForSpider : DbMigration
    {
        public override void Up()
        {
            /*
            AddColumn("dbo.Groups", "ClosedRegistation", c => c.Boolean());

            AddColumn("dbo.Groups", "IdFinalFight", c => c.Int());
            CreateIndex("dbo.Groups", "IdFinalFight");
            AddForeignKey("dbo.Groups", "IdFinalFight", "dbo.Groups", "Id");
            */
        }
        
        public override void Down()
        {

        }
    }
}
