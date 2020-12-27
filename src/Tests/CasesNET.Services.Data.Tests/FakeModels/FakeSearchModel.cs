using AutoMapper;
using CasesNET.Data.Models;
using CasesNET.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasesNET.Services.Data.Tests.FakeModels
{
    public class FakeSearchModel : IMapFrom<Case>, IMapTo<Case>, IHaveCustomMappings
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public string DeviceName { get; set; }

        public string ManufacturerName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Case, FakeSearchModel>()
                .ForMember(m => m.ManufacturerName, cfg =>
                 cfg.MapFrom(x => x.Device.Manufactorer.Name));
        }
    }
}
