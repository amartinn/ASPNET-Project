namespace CasesNET.Web.ViewModels.Devices
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class DeviceSelectListModel : IMapFrom<Device>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
