namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data;
    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Categories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly ApplicationDbContext db;
        private readonly IFileService fileService;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository, ApplicationDbContext db, IFileService fileService)
        {
            this.categoryRepository = categoryRepository;
            this.db = db;
            this.fileService = fileService;
        }

        public async Task CreateAsync(CategoryCreateInputModel model, string imagePath)
        {
            var spliitedImageArgs = model.Image.FileName.Split('.');
            var imageName = spliitedImageArgs[0];
            var imageExtension = spliitedImageArgs[1];
            var item = new Category
            {
                Name = model.Name,
                Image = new Image
                {
                    Url = imageName,
                    Extension = imageExtension,
                },
            };
            await this.fileService.SaveImageToDiskAsync(model.Image, imagePath);
            await this.categoryRepository.AddAsync(item);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var category = this.categoryRepository
                .All()
                .FirstOrDefault(x => x.Id == id);
            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
            => this.categoryRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

        public T GetById<T>(string id)
            => this.categoryRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();

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

        public async Task UpdateAsync(CategoryEditInputModel model, string imagePath)
        {
            var item = this.categoryRepository.All().FirstOrDefault(x => x.Id == model.Id);
            item.Name = model.Name;
            if (model.Image != null)
            {
                this.fileService.DeleteImageFromDisc(imagePath, item.Image.Url, item.Image.Extension);
                var spliitedImageArgs = model.Image.FileName.Split('.');
                var imageName = spliitedImageArgs[0];
                var imageExtension = spliitedImageArgs[1];
                item.Image = new Image
                {
                    Url = imageName,
                    Extension = imageExtension,
                };
                await this.fileService.SaveImageToDiskAsync(model.Image, imagePath);
            }

            this.categoryRepository.Update(item);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
