namespace CasesNET.Web.ViewModels.Administration.Orders
{
    using System.Linq;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string OrderStatus { get; set; }
    }
}
