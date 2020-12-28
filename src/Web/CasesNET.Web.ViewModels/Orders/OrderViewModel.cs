namespace CasesNET.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Cart;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string OrderStatus { get; set; }

        public string CreatedOn { get; set; }

        public int Id { get; set; }

        public IEnumerable<CartItemViewModel> Items { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(
                    m => m.OrderStatus,
                    opt => opt.MapFrom(x => x.OrderStatus.ToString()))
                .ForMember(
                    m => m.CreatedOn,
                    opt => opt.MapFrom(x => x.CreatedOn.ToString("d")));
        }
    }
}
