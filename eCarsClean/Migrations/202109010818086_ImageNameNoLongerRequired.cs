namespace eCarsClean.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageNameNoLongerRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Image", c => c.String(nullable: false));
        }
    }
}
