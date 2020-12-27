namespace CasesNET.Services.Data
{
    using CasesNET.Web.ViewModels.Administration.Devices;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDeviceService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);

        Task CreateAsync(DeviceCreateInputModel model);

        Task UpdateAsync(DeviceEditInputModel model);

        Task DeleteByIdAsync(string id);
    }
}
