namespace TournamentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullsForFightTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fights", "TimeEnd", c => c.DateTime());
            AlterColumn("dbo.Fights", "Overtime", c => c.Boolean());
            AlterColumn("dbo.Fights", "PointsFirst", c => c.Int());
            AlterColumn("dbo.Fights", "PointsSecond", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fights", "PointsSecond", c => c.Int(nullable: false));
            AlterColumn("dbo.Fights", "PointsFirst", c => c.Int(nullable: false));
            AlterColumn("dbo.Fights", "Overtime", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Fights", "TimeEnd", c => c.DateTime(nullable: false));
        }
    }
}
