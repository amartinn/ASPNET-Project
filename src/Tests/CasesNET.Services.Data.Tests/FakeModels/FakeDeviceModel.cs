using CasesNET.Data.Models;
using CasesNET.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasesNET.Services.Data.Tests.FakeModels
{
    public class FakeDeviceModel : IMapFrom<Device>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
