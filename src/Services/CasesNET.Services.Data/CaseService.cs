namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CasesNET.Data;
    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Cases;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class CaseService : ICaseService
    {
        private readonly IDeletableEntityRepository<Case> caseRepository;

        public CaseService(IDeletableEntityRepository<Case> caseRepository)
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

        public IEnumerable<T> GetLatest<T>(int count = 12)
            => this.caseRepository
             .AllAsNoTracking()
             .OrderByDescending(x => x.CreatedOn)
            .Take(count)
             .To<T>();

        public IEnumerable<T> GetAllByManufacturerId<T>(string id, int page, int itemsPerPage = 12)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Device.Manufactorer.Id == id)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<T>();

        public IEnumerable<T> GetAllByCategoryId<T>(string categoryId, int page = 1, int itemsPerPage = 12)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Category.Id == categoryId)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<T>();

        public int GetCountByManufacturer(string manufacturerId)
            => this.caseRepository
            .AllAsNoTracking()
            .Where(x => x.Device.ManufacturerId == manufacturerId)
            .Count();

        public IEnumerable<T> GetAll<T>()
            => this.caseRepository
            .AllAsNoTracking()
            .To<T>();

        public async Task DeleteByIdAsync(string id)
        {
            var item = this.caseRepository.All().FirstOrDefault(x => x.Id == id);
            this.caseRepository.Delete(item);
            await this.caseRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditViewModel model)
        {
            var item = this.caseRepository.All().FirstOrDefault(x => x.Id == model.Id);
            item.Name = model.Name;
            item.CategoryId = model.CategoryId;
            item.DeviceId = model.DeviceId;
            item.Description = model.Description;
            item.Price = model.Price;

            this.caseRepository.Update(item);
            await this.caseRepository.SaveChangesAsync();
        }

        private async Task SaveImageToDiskAsync(IFormFile file, string path)
        {
            string filePath = Path.Combine(path, file.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
