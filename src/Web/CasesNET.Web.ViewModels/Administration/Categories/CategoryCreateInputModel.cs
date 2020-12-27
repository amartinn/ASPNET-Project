namespace CasesNET.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static CasesNET.Data.Common.Validation.Category;

    public class CategoryCreateInputModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
