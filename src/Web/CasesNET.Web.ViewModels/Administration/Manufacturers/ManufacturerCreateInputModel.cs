namespace CasesNET.Web.ViewModels.Administration.Manufacturers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    using static CasesNET.Data.Common.Validation.Manufacturer;

    public class ManufacturerCreateInputModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
