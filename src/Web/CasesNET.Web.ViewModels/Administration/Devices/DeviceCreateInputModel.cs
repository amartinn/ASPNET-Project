namespace CasesNET.Web.ViewModels.Administration.Devices
{
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static CasesNET.Data.Common.Validation.Device;

    public class DeviceCreateInputModel : IMapFrom<Device>
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string ManufacturerId { get; set; }

        public SelectList Manufacturers { get; set; }
    }
}
