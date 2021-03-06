﻿namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static CasesNET.Data.Common.Validation.Case;

    public class CreateCaseInputModel : IMapTo<Case>
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Devices { get; set; }
    }
}
