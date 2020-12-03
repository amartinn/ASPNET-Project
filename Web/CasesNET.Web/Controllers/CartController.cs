namespace CasesNET.Web.Controllers
{
    using System.Linq;

    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var cartItems = this.cartService.GetAllItemsByUserId<CartItemViewModel>(userId);
            var viewModel = new CartItemListingViewModel
            {
                Items = cartItems,
                TotalPrice = cartItems.Sum(x => x.CasePrice * x.Quantity),
            };
            return this.View(viewModel);
        }
    }
}
