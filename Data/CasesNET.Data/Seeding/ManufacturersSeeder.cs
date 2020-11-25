namespace CasesNET.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;

    public class ManufacturersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            var manufacturers = new List<string>
            {
                "Samsung",
                "Apple",
                "Huawei",
                "Xiaomi",
                "Sony",
                "Asus",
                "Nokia",
                "HTC",
                "Motorola",
            };

            foreach (var manufacturerName in manufacturers)
            {
                var manufacturer = new Manufacturer
                {
                    Name = manufacturerName,
                };
                await dbContext.Manufacturers.AddAsync(manufacturer);
            }
        }
    }
}
