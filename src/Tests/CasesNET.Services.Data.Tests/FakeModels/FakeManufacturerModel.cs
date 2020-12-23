namespace CasesNET.Services.Data.Tests.FakeModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeManufacturerModel : IMapFrom<Manufacturer>
    {
        public string Id { get; set; }
    }
}
