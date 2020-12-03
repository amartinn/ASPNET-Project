namespace CasesNET.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using AutoMapper;
    using CasesNET.Data.Common.Models;
    using CasesNET.Services.Mapping;

    public class CartItem : BaseDeletableModel<string>
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Case))]
        public string CaseId { get; set; }

        public virtual Case Case { get; set; }

        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
