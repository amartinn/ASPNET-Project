namespace CasesNET.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    public class CartItemListingViewModel
    {
        public IEnumerable<CartItemViewModel> Items { get; set; }

        public decimal TotalPrice { get; set; }

        public string CartId { get; set; }
    }
}
