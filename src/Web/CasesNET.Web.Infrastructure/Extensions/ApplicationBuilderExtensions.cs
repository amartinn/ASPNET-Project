namespace CasesNET.Web.Infrastructure.Extensions
{
    using CasesNET.Data;
    using CasesNET.Data.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void SeedAndMigrateDatabase(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
            new ApplicationDbContextSeeder().SeedAsync(dbContext, services.ServiceProvider).GetAwaiter().GetResult();
        }
    }
}
