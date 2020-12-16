namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICaseService
    {
        T GetById<T>(string id);

        Task CreateAsync();

        int GetItemsCountByCategoryId(string categoryId);

        int GetCountByManufacturer(string manufacturerId);

        IEnumerable<T> GetBestSellers<T>(int count = 4);

        IEnumerable<T> GetAllByCategory<T>(string categoryId, int page = 1, int itemsPerPage = 12);

        IEnumerable<T> GetLatest<T>(int count = 12);

        IEnumerable<T> GetByManufacturerId<T>(string manufacturerId, int page = 1, int itemsPerPage = 12);
    }
}
