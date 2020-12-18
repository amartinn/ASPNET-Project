namespace CasesNET.Web.ViewModels.Cases
{
    using System.Collections.Generic;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Shared;

    public class CaseDetailsViewModel : IMapFrom<Case>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string DeviceName { get; set; }

        public string ManufacturerName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CaseViewModel> RelatedCases { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseDetailsViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"))
                  .ForMember(m => m.ManufacturerName, opt =>
                  opt.MapFrom(x => x.Device.Manufactorer.Name));
    }
}
