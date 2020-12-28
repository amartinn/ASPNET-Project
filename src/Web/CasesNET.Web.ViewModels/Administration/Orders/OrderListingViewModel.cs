namespace CasesNET.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    public class OrderListingViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
