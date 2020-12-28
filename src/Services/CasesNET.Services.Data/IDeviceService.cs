namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Administration.Devices;

    public interface IDeviceService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);

        Task CreateAsync(DeviceCreateInputModel model);

        Task UpdateAsync(DeviceEditInputModel model);

        Task DeleteByIdAsync(string id);
    }
}
