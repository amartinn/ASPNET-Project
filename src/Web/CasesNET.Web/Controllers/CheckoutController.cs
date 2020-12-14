namespace CasesNET.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cart;
    using CasesNET.Web.ViewModels.Checkout;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;
        private readonly IOrderService orderService;

        public CheckoutController(
            UserManager<ApplicationUser> userManager,
            ICartService cartService,
            IOrderService orderService)
        {
            this.userManager = userManager;
            this.cartService = cartService;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var items = this.cartService.GetAllItemsByUserId<CartItemViewModel>(user.Id);
                var viewModel = new CheckoutInputModel()
                {
                    UserId = user.Id,
                    CartId = items.First().CartId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Adress = user.LastName,
                    City = user.City,
                    Country = user.Country,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    CartItemsCount = this.cartService.GetItemsCountByUserId(user.Id),
                    CartItems = items,
                };
                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.Redirect("/");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.orderService.CreateAsync(model);
            await this.cartService.RemoveCartByIdAndUserIdAsync(model.CartId, model.UserId);
            return this.Redirect("/");
        }
    }
}
