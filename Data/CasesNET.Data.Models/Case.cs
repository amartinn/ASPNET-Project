namespace CasesNET.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Data.Common.Models;

    public class Case : BaseDeletableModel<string>
    {
        public Case()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ImageId { get; set; }

        public Image Image { get; set; }

        [Required]
        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
