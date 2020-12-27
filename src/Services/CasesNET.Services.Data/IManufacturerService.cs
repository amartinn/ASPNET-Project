namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Administration.Manufacturers;

    public interface IManufacturerService
    {
        IEnumerable<T> GetAll<T>();

        string GetNameById(string id);

        T GetById<T>(string id);

        Task CreateAsync(ManufacturerCreateInputModel model, string imagePath);

        Task UpdateAsync(ManufacturerEditInputModel model, string imagePath);

        Task DeleteByIdAsync(string id);
    }
}
