namespace CasesNET.Web.ViewModels.Home
{
    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Category, CategoryViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"));
    }
}
