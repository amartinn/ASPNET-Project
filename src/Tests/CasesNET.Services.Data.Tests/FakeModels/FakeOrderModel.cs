namespace CasesNET.Services.Data.Tests.FakeModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeOrderModel : IMapFrom<Order>
    {
        public string OrderedById { get; set; }
    }
}
