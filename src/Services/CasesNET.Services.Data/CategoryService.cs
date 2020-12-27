namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Data;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly ApplicationDbContext db;
        private readonly IRepository<Case> caseRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository, ApplicationDbContext db)
        {
            this.categoryRepository = categoryRepository;
            this.db = db;
        }

        public IEnumerable<T> GetAll<T>()
            => this.categoryRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

        // TODO: Refactor when implement Order Entity.
        public IEnumerable<T> GetMostSold<T>(int count = 4)
        {
            const string sqlcommand = @"
                       select cat.Id as Id, 
                    cat.CreatedOn as  CreatedOn, 
                    cat.ModifiedOn as ModifiedOn ,
                    cat.DeletedOn as DeletedOn,
                    cat.IsDeleted as IsDeleted,
                    cat.Name as Name,
                    cat.ImageId as ImageId
                    from Orders as o
                    join Carts as c 
                    on c.Id = o.CartId
                    join cartitems as ct 
                    on ct.CartId = c.Id
                    join Cases as cases
                    on ct.CaseId = cases.Id
                    join Categories as cat
                    on cases.CategoryId = cat.Id
                    where (cases.CartItemId is not null)
                    GROUP BY cat.Id,cat.CreatedOn,cat.ModifiedOn,cat.DeletedOn,cat.isDeleted,cat.Name,cat.ImageId
                            ";

            var items = this.db.Categories
                .FromSqlRaw(sqlcommand)
                .Take(count)
                .To<T>()
                .ToList();

            return items;
       }

        public string GetNameById(string id)
            => this.categoryRepository
            .AllAsNoTracking()
            .FirstOrDefault(x => x.Id == id)
            .Name;
    }
}
