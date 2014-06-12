namespace OpenGovApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ServiceId = c.String(),
                        Reference = c.String(),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        Location_Address_Uprn = c.Long(),
                        Location_Address_Usrn = c.Long(),
                        Location_Address_Property = c.String(maxLength: 200),
                        Location_Address_Street = c.String(maxLength: 200),
                        Location_Address_Locality = c.String(maxLength: 200),
                        Location_Address_Town = c.String(maxLength: 200),
                        Location_Address_PostTown = c.String(maxLength: 200),
                        Location_Address_County = c.String(maxLength: 200),
                        Location_Address_PostCode = c.String(maxLength: 10),
                        Location_Address_Country = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name_Title = c.String(maxLength: 10),
                        Name_FirstName = c.String(maxLength: 100),
                        Name_LastName = c.String(maxLength: 100),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        ObjectId = c.String(),
                        ObjectType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceAttributeAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceAttributeId = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Name = c.String(),
                        HelpText = c.String(),
                        Type = c.Int(nullable: false),
                        Required = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceAttributeValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Order = c.Int(nullable: false),
                        ServiceAttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LgslId = c.Int(nullable: false),
                        Category = c.String(),
                        Title = c.String(),
                        Summary = c.String(),
                        Content = c.String(),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
            DropTable("dbo.ServiceAttributeValues");
            DropTable("dbo.ServiceAttributes");
            DropTable("dbo.ServiceAttributeAttributes");
            DropTable("dbo.Relationships");
            DropTable("dbo.People");
            DropTable("dbo.Assets");
            DropTable("dbo.Accounts");
        }
    }
}
