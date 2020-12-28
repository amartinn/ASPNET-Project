namespace CasesNET.Services.Data.Tests.FakeModels
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeDeviceModel : IMapFrom<Device>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
