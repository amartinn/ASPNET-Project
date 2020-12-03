namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAll<T>()
            => this.categoryRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

        // TODO: Refactor when implement Order Entity.
        public IEnumerable<T> GetMostSold<T>(int count = 4)
            => this.categoryRepository
                .AllAsNoTracking()
                .Where(x => x.CreatedOn <= DateTime.UtcNow)
                .Take(count)
                .To<T>()
                .ToList();
    }
}
