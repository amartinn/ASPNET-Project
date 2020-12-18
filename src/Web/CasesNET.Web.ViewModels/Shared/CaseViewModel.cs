namespace CasesNET.Web.ViewModels.Shared
{
    using AutoMapper;
    using CasesNET.Common;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CaseViewModel : SEOFriendlyModel, IMapFrom<Case>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public override string FriendlyUrlName => this.Name;

        public override string FriendlyUrlPrefix => "Cases";

        public string ImageUrl { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerId { get; set; }

        public string DeviceName { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"))
                  .ForMember(m => m.ManufacturerName, opt =>
                  opt.MapFrom(x => x.Device.Manufactorer.Name))
                 .ForMember(m => m.ManufacturerId, opt =>
                 opt.MapFrom(x => x.Device.Manufactorer.Id))
                  .ForMember(m => m.CategoryName, opt =>
                  opt.MapFrom(x => x.Category.Name));
    }
}
