namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class EditViewModel : IMapFrom<Case>, IMapTo<Case>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public string DeviceId { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"));
    }
}
