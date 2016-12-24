namespace ScheduleMVC_Update.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DaySet", "ScheduleMainScheduleMainId", "dbo.ScheduleMainSet");
            DropIndex("dbo.DaySet", new[] { "ScheduleMainScheduleMainId" });
            AddColumn("dbo.ScheduleMainSet", "DaysOfWeek", c => c.String());
            DropColumn("dbo.ScheduleMainSet", "Attachment");
            DropTable("dbo.DaySet");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DaySet",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.String(nullable: false),
                        ScheduleMainScheduleMainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            AddColumn("dbo.ScheduleMainSet", "Attachment", c => c.String());
            DropColumn("dbo.ScheduleMainSet", "DaysOfWeek");
            CreateIndex("dbo.DaySet", "ScheduleMainScheduleMainId");
            AddForeignKey("dbo.DaySet", "ScheduleMainScheduleMainId", "dbo.ScheduleMainSet", "ScheduleId", cascadeDelete: true);
        }
    }
}
