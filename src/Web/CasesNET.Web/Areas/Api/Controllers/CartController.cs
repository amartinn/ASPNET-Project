namespace CasesNET.Web.Areas.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CartController : ApiController
    {
        private readonly ICaseService caseService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;

        public CartController(
            ICaseService caseService,
            UserManager<ApplicationUser> userManager,
            ICartService cartService)
        {
            this.caseService = caseService;
            this.userManager = userManager;
            this.cartService = cartService;
        }

        [HttpPost]
        [Route(nameof(AddItem))]
        public async Task<IActionResult> AddItem(string id)
        {
            var caseExists = this.caseService.Exists(id);
            var userId = this.userManager.GetUserId(this.User);
            if (caseExists)
            {
                await this.cartService.AddItemByIdAndUserIdAsync(id, userId);
                return this.Ok();
            }

            return this.NotFound();
        }

        [HttpGet]
        [Route(nameof(Count))]
        public int Count()
        {
            var userId = this.userManager.GetUserId(this.User);
            return this.cartService.GetItemsCountByUserId(userId);
        }

        [HttpPost]
        [Route(nameof(RemoveItem))]
        public async Task<IActionResult> RemoveItem(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            try
            {
                await this.cartService.RemoveItemByIdAndUserIdAsync(id, userId);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
