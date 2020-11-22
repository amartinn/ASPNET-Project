namespace CasesNET.Data.Models
{
    using System;

    using CasesNET.Data.Common.Models;

    public class Case : BaseDeletableModel<string>
    {
        public Case()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ImageUrl { get; set; }

        public Brand Brand { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
