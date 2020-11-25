namespace CasesNET.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;

    public class CasesSeeder : ISeeder
    {
        private const int TotalSeededCases = 100;
        private static readonly Random Random = new Random();

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cases.Any())
            {
                return;
            }

            var devisesIds = dbContext.Devices.Select(x => x.Id).ToList();
            var categoriesIds = dbContext.Categories.Select(x => x.Id).ToList();
            for (int i = 0; i < TotalSeededCases; i++)
            {
                var deviceId = devisesIds[Random.Next(1, devisesIds.Count)];
                var categoryId = categoriesIds[Random.Next(1, categoriesIds.Count)];
                var @case = new Case
                {
                    CategoryId = categoryId,
                    Description = $"description {i}",
                    DeviceId = deviceId,
                    Price = (decimal)((Random.NextDouble() * (15.0 - 20.0)) + 20.0),
                    Image = new Image
                    {
                        Extension = "jpg",
                        Url = "~/images/test-case",
                    },
                };
                await dbContext.Cases.AddAsync(@case);
            }
        }
    }
}
