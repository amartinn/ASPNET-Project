namespace CasesNET.Web.ViewModels.Administration.Manufacturers
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class ManufacturerSelectListModel : IMapFrom<Manufacturer>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
