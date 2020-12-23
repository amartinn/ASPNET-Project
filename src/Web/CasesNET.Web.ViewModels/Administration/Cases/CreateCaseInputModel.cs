namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateCaseInputModel : IMapTo<Case>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
