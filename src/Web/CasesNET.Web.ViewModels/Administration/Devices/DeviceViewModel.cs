namespace CasesNET.Web.ViewModels.Administration.Devices
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class DeviceViewModel : IMapFrom<Device>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ManuFacturerName { get; set; }
    }
}
