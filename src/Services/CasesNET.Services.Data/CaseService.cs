namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CaseService : ICaseService
    {
        private readonly IRepository<Case> caseRepository;

        public CaseService(IRepository<Case> caseRepository)
        {
            this.caseRepository = caseRepository;
        }

        public Task CreateAsync()
        {
            throw new NotImplementedException();
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
    }
}
