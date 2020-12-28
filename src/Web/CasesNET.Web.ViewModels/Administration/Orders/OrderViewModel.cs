namespace CasesNET.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Cart;

    public class OrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string OrderStatus { get; set; }

        public IEnumerable<CartItemViewModel> Items { get; set; }
    }
}
