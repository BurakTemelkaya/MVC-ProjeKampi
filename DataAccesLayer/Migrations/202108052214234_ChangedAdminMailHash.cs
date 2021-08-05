namespace DataAccesLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAdminMailHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminMailHash", c => c.Binary());
            AddColumn("dbo.Admins", "AdminMailSalt", c => c.Binary());
            DropColumn("dbo.Admins", "AdminMail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "AdminMail", c => c.Binary());
            DropColumn("dbo.Admins", "AdminMailSalt");
            DropColumn("dbo.Admins", "AdminMailHash");
        }
    }
}
