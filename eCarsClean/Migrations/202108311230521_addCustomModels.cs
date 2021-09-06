namespace eCarsClean.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarModel = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        MilesPerCharge = c.Double(nullable: false),
                        Image = c.String(nullable: false),
                        Video = c.String(nullable: false),
                        Likes = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerName = c.String(),
                        AddressLine1 = c.String(),
                        City = c.String(),
                        Postcode = c.String(),
                        Tel = c.String(),
                        Email = c.String(),
                        WebSite = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Cars", new[] { "ManufacturerId" });
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Cars");
        }
    }
}
