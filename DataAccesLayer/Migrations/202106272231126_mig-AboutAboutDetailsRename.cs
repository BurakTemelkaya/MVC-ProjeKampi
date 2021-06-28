namespace DataAccesLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migAboutAboutDetailsRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "AboutDetails1", c => c.String(maxLength: 1000));
            DropColumn("dbo.Abouts", "AboutDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abouts", "AboutDetails", c => c.String(maxLength: 1000));
            DropColumn("dbo.Abouts", "AboutDetails1");
        }
    }
}
