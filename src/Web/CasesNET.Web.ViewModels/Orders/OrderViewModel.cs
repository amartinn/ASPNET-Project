namespace CasesNET.Web.ViewModels.Orders
{
    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string OrderStatus { get; set; }

        public string CreatedOn { get; set; }

        public int Id { get; set; }

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
