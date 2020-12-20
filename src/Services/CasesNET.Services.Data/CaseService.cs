namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Cases;
    using Microsoft.AspNetCore.Http;

    public class CaseService : ICaseService
    {
        private readonly IRepository<Case> caseRepository;

        public CaseService(IRepository<Case> caseRepository)
        {
            this.caseRepository = caseRepository;
        }

        public async Task CreateAsync(CreateCaseInputModel model, string imagePath)
        {
            var spliitedImageArgs = model.Image.FileName.Split('.');
            var imageName = spliitedImageArgs[0];
            var imageExtension = spliitedImageArgs[1];
            var item = new Case
            {
                CategoryId = model.CategoryId,
                DeviceId = model.DeviceId,
                Price = model.Price,
                Name = model.Name,
                Description = model.Description,
                Image = new Image
                {
                    Url = imageName,
                    Extension = imageExtension,
                },
            };
            await this.SaveImageToDiskAsync(model.Image, imagePath);
            await this.caseRepository.AddAsync(item);
            await this.caseRepository.SaveChangesAsync();
        }

        // TODO: Refactor when order Entity is added.
        public IEnumerable<T> GetBestSellers<T>(int count = 4)
            => this.caseRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CartItem.Cart.Items.Count())
            .To<T>()
            .Take(count)
            .ToList();

        public T GetById<T>(string id)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();

        public int GetItemsCountByCategoryId(string categoryId)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.CategoryId == categoryId)
            .Count();

        public IEnumerable<T> GetAllByCategory<T>(string categoryId, int page = 1, int itemsPerPage = 12)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.CategoryId == categoryId)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<T>()
            .ToList();

        public IEnumerable<T> GetLatest<T>(int count = 12)
            => this.caseRepository
            .AllAsNoTracking()
            .OrderBy(x => x.CreatedOn)
            .Take(count)
            .To<T>()
            .ToList();

        public IEnumerable<T> GetByManufacturerId<T>(string id, int page, int itemsPerPage = 12)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Device.Manufactorer.Id == id)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<T>()
            .ToList();

        public int GetCountByManufacturer(string manufacturerId)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Device.ManufacturerId == manufacturerId)
            .Count();

        private async Task SaveImageToDiskAsync(IFormFile file, string path)
        {
            string filePath = Path.Combine(path, file.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
