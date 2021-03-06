﻿namespace CasesNET.Services.Data.Tests.FakeModels
{
    using System;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeCaseModel : IMapFrom<Case>
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DeviceManufacturerId { get; set; }
    }
}
