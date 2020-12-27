namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Manufacturers;
    using Microsoft.AspNetCore.Http;

    public class ManufacturerService : IManufacturerService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturerRepository;

        public ManufacturerService(IDeletableEntityRepository<Manufacturer> manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }

        public IEnumerable<T> GetAll<T>()
            => this.manufacturerRepository
            .AllAsNoTracking()
            .To<T>()
            .ToList();

        public string GetNameById(string id)
            => this.manufacturerRepository
             .AllAsNoTracking()
             .FirstOrDefault(x => x.Id == id)
             ?.Name;

        public async Task UpdateAsync(ManufacturerEditInputModel model, string imagePath)
        {
            var item = this.manufacturerRepository.All().FirstOrDefault(x => x.Id == model.Id);
            item.Name = model.Name;
            if (model.Image == null)
            {
                this.DeleteImageFromDisc(imagePath, item.Image.Url, item.Image.Extension);
                var spliitedImageArgs = model.Image.FileName.Split('.');
                var imageName = spliitedImageArgs[0];
                var imageExtension = spliitedImageArgs[1];
                item.Image = new Image
                {
                    Url = imageName,
                    Extension = imageExtension,
                };
                await this.SaveImageToDiskAsync(model.Image, imagePath);
            }

            this.manufacturerRepository.Update(item);
            await this.manufacturerRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(ManufacturerCreateInputModel model, string imagePath)
        {
            var spliitedImageArgs = model.Image.FileName.Split('.');
            var imageName = spliitedImageArgs[0];
            var imageExtension = spliitedImageArgs[1];
            var item = new Manufacturer
            {
                Name = model.Name,
                Image = new Image
                {
                    Url = imageName,
                    Extension = imageExtension,
                },
            };
            await this.SaveImageToDiskAsync(model.Image, imagePath);
            await this.manufacturerRepository.AddAsync(item);
            await this.manufacturerRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var item = this.manufacturerRepository.All().FirstOrDefault(x => x.Id == id);
            this.manufacturerRepository.Delete(item);
            await this.manufacturerRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
           => this.manufacturerRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();

        private async Task SaveImageToDiskAsync(IFormFile file, string path)
        {
            string filePath = Path.Combine(path, file.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        private void DeleteImageFromDisc(string path, string imageName, string imageExtension)
        {
            var imagePath = Path.Combine(path, $"{imageName}.{imageExtension}");
            File.Delete(imagePath);
        }
    }
}
