﻿namespace CasesNET.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static CasesNET.Data.Common.Validation.Category;

    public class CategoryEditInputModel : IMapFrom<Category>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Category, CategoryEditInputModel>()
                 .ForMember(m => m.Image, opt => opt.Ignore());
    }
}
