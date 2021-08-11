namespace DataAccesLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFileAddedImageDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageFiles", "ImageDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageFiles", "ImageDate");
        }
    }
}
