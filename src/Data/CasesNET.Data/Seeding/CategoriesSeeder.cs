﻿namespace CasesNET.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoriesNames = new List<string>
            {
                "shades of autumn",
                "street style",
                "summer vibes",
                "art",
                "swag",
                "geometric",
                "games",
                "family and friends",
                "quotes",
                "flowers",
                "animals",
                "automobiles",
                "sports",
                "movies",
                "music",
            };
            foreach (var categoryName in categoriesNames)
            {
                var url = categoryName.Replace(' ', '-');
                url += "-category";
                var category = new Category
                {
                    Name = categoryName,
                    Image = new Image
                    {
                        Extension = "jpg",
                        Url = url,
                    },
                };
                await dbContext.Categories.AddAsync(category);
            }
        }
    }
}
