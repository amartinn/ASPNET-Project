namespace CasesNET.Services.Data.Tests.FakeModels
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeOrderModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string OrderedById { get; set; }
    }
}
