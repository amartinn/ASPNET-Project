namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class DeviceService : IDeviceService
    {
        private readonly IDeletableEntityRepository<Device> deviceRepository;

        public DeviceService(IDeletableEntityRepository<Device> deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public IEnumerable<T> GetAll<T>()
            => this.deviceRepository
            .AllAsNoTracking()
            .To<T>();
    }
}
