namespace CasesNET.Web.ViewModels.Shared
{
    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CaseViewModel : IMapFrom<Case>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string BrandName { get; set; }

        public string DeviceName { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"))
                  .ForMember(m => m.BrandName, opt =>
                  opt.MapFrom(x => x.Device.Manufactorer.Name))
                  .ForMember(m => m.CategoryName, opt =>
                  opt.MapFrom(x => x.Category.Name));
    }
}
