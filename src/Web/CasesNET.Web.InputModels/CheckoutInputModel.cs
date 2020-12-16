namespace CasesNET.Web.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.Infrastructure.Attributes;
    using CasesNET.Web.ViewModels.Cart;
    using static CasesNET.Data.Common.Validation.Checkout;

    public class CheckoutInputModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
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

        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        public int CartItemsCount { get; set; }
    }
}
