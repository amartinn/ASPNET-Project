namespace CasesNET.Web.Areas.Identity.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Identity")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult ByUser(string id)
        {
            var viewModel = new OrderListingViewModel
            {
                Orders = this.orderService.GetAllByUserId<OrderViewModel>(id),
            };
            return this.View(viewModel);
        }
    }
}
