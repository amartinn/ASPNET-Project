namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Administration.Cases;

    using static CasesNET.Common.GlobalConstants.Paging;

    public interface ICaseService
    {
        T GetById<T>(string id);

        Task CreateAsync(CreateCaseInputModel model, string imagePath);

        int GetItemsCountByCategoryId(string categoryId);

        int GetCountByManufacturer(string manufacturerId);

        IEnumerable<T> GetAllByCategoryId<T>(string categoryId, int page = 1, int itemsPerPage = ItemsPerPage);

        IEnumerable<T> GetLatest<T>(int count = 12);

        IEnumerable<T> GetAllByManufacturerId<T>(string manufacturerId, int page = 1, int itemsPerPage = ItemsPerPage);

        IEnumerable<T> GetAll<T>();

        Task DeleteByIdAsync(string id);

        Task UpdateAsync(EditViewModel model);
    }
}
