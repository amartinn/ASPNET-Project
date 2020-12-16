namespace CasesNET.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderListingViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
