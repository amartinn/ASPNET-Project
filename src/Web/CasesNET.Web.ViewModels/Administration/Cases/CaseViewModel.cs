namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CaseViewModel : IMapFrom<Case>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string DeviceName { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Case, CaseViewModel>()
                  .ForMember(m => m.ImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Image.Url}.{x.Image.Extension}"));
    }
}
