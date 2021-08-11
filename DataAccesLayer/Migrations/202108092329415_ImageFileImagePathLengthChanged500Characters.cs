namespace DataAccesLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFileImagePathLengthChanged500Characters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageFiles", "ImagePath", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageFiles", "ImagePath", c => c.String(maxLength: 250));
        }
    }
}
