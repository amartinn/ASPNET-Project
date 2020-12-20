﻿using CasesNET.Data.Models;
using CasesNET.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CasesNET.Web.ViewModels.Administration.Cases
{
    public class CreateCaseInputModel : IMapTo<Case>
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
