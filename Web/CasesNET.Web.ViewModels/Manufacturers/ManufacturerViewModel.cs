namespace CasesNET.Web.ViewModels.Manufacturers
{
    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class ManufacturerViewModel : IMapFrom<Manufacturer>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Manufacturer, ManufacturerViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"));
    }
}
