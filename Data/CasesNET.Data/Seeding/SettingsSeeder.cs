namespace CasesNET.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;

    internal class SettingsSeeder : ISeeder
    {
        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }
    }
}
