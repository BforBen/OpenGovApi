namespace OpenGovApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Services", "ServiceCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "ServiceCategoryId");
            AddForeignKey("dbo.Services", "ServiceCategoryId", "dbo.ServiceCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.Services", "LgslId");
            DropColumn("dbo.Services", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Category", c => c.String());
            AddColumn("dbo.Services", "LgslId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Services", "ServiceCategoryId", "dbo.ServiceCategories");
            DropIndex("dbo.Services", new[] { "ServiceCategoryId" });
            DropColumn("dbo.Services", "ServiceCategoryId");
            DropTable("dbo.ServiceCategories");
        }
    }
}
