namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static CasesNET.Data.Common.Validation.Case;

    public class CaseEditInputModel : IMapFrom<Case>, IMapTo<Case>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Devices { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"));
    }
}
