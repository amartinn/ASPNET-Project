namespace CasesNET.Web.ViewModels.Home
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class ManufacturerListingViewModel : IMapFrom<Manufacturer>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
