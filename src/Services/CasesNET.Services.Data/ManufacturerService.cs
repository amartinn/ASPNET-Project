namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

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
    }
}
