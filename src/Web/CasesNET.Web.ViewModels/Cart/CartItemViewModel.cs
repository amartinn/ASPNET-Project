namespace CasesNET.Web.ViewModels.Cart
{
    using System;

    using AutoMapper;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CartItemViewModel : IMapFrom<CartItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CaseName { get; set; }

        public string CaseImageUrl { get; set; }

        public string CaseDeviceName { get; set; }

        public string CaseManufacturerName { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public decimal CasePrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
              => configuration.CreateMap<CartItem, CartItemViewModel>()
                  .ForMember(m => m.CaseImageUrl, opt =>
                   opt.MapFrom(x => $"{x.Case.Image.Url}.{x.Case.Image.Extension}"))
                  .ForMember(m => m.CaseManufacturerName, opt =>
                  opt.MapFrom(x => x.Case.Device.Manufacturer.Name))
                  .ForMember(m => m.CaseDeviceName, opt =>
                   opt.MapFrom(x => x.Case.Device.Name));
    }
}
