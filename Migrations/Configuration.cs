namespace OpenGovApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OpenGovApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<OpenGovApi.Models.OpenGovContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OpenGovApi.Models.OpenGovContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.ServiceCategories.AddOrUpdate(
              s => s.Id,
              new ServiceCategory { Id = 526, Name = "Refuse - household waste - domestic bins" }
            );

            context.Services.AddOrUpdate(
              s => s.Id,
              new Service { Id = "1", Content = "Test1 content", ServiceCategoryId = 526, Summary = "How to report a missed bin.", Title = "Report a missed bin", Updated = DateTime.Now },
              new Service { Id = "2", Content = "Test2 content", ServiceCategoryId = 526, Summary = "How to report a damaged bin.", Title = "Report a damaged bin", Updated = DateTime.Now }
            );
            
        }
    }
}
