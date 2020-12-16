namespace CasesNET.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using CasesNET.Data.Common.Models;
    using CasesNET.Data.Models.Enum;
    using CasesNET.Services.Mapping;

    public class Order : BaseModel<int>
    {
        [ForeignKey(nameof(OrderedBy))]
        public string OrderedById { get; set; }

        public virtual ApplicationUser OrderedBy { get; set; }

        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
