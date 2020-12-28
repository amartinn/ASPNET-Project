namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Administration.Devices;

    public class DeviceService : IDeviceService
    {
        private readonly IDeletableEntityRepository<Device> deviceRepository;

        public DeviceService(IDeletableEntityRepository<Device> deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public async Task CreateAsync(DeviceCreateInputModel model)
        {
            var device = new Device
            {
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
            };
            await this.deviceRepository.AddAsync(device);
            await this.deviceRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var device = this.deviceRepository.All()
                 .FirstOrDefault(x => x.Id == id);

            this.deviceRepository.Delete(device);
            await this.deviceRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
            => this.deviceRepository
            .AllAsNoTracking()
            .To<T>();

        public T GetById<T>(string id)
            => this.deviceRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();

        public async Task UpdateAsync(DeviceEditInputModel model)
        {
            var device = this.deviceRepository.All()
                .FirstOrDefault(x => x.Id == model.Id);
            device.ManufacturerId = model.ManufacturerId;
            device.Name = model.Name;

            this.deviceRepository.Update(device);
            await this.deviceRepository.SaveChangesAsync();
        }
    }
}
