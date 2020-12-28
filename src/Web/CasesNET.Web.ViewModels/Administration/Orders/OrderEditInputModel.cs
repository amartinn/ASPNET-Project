namespace CasesNET.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CasesNET.Attributes;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static CasesNET.Data.Common.Validation.Checkout;

    public class OrderEditInputModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(AdressMaxLength)]
        public string Adress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Country]
        public string Country { get; set; }

        [Required]
        [City]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public SelectList Statuses { get; set; }

        public IEnumerable<CartItemViewModel> Items { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Order, OrderEditInputModel>()
            .ForMember(m => m.FirstName, cfg => cfg.MapFrom(x => x.OrderedBy.FirstName))
            .ForMember(m => m.LastName, cfg => cfg.MapFrom(x => x.OrderedBy.LastName))
            .ForMember(m => m.Adress, cfg => cfg.MapFrom(x => x.OrderedBy.Adress))
            .ForMember(m => m.PhoneNumber, cfg => cfg.MapFrom(x => x.OrderedBy.PhoneNumber))
            .ForMember(m => m.Country, cfg => cfg.MapFrom(x => x.OrderedBy.Country))
            .ForMember(m => m.City, cfg => cfg.MapFrom(x => x.OrderedBy.City))
            .ForMember(m => m.Email, cfg => cfg.MapFrom(x => x.OrderedBy.Email))
            .ForMember(m => m.OrderStatus, cfg => cfg.MapFrom(x => x.OrderStatus.ToString()));
    }
}
