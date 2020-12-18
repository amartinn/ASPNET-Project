namespace CasesNET.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CasesNET.Data.Common.Models;

    public class Case : BaseDeletableModel<string>
    {
        public Case()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        [Required]
        public string DeviceId { get; set; }

        public virtual Device Device { get; set; }

        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(CartItem))]
        public string CartItemId { get; set; }

        public virtual CartItem CartItem { get; set; }
    }
}
