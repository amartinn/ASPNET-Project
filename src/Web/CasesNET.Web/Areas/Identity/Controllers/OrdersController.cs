namespace CasesNET.Web.Areas.Identity.Controllers
{
    using System.Threading.Tasks;

    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cart;
    using CasesNET.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Identity")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService orderService, ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ByUser()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var viewModel = new OrderListingViewModel
            {
                Orders = this.orderService.GetAllByUserId<OrderViewModel>(userId),
            };
            foreach (var order in viewModel.Orders)
            {
                order.Items = this.orderService.GetAllItemsByOrderId<CartItemViewModel>(order.Id);
            }

            return this.View(viewModel);
        }
    }
}
