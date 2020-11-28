namespace CasesNET.Web.Areas.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static CasesNET.Common.GlobalConstants.ShoppingCart;

    public class CartController : ApiController
    {
        private readonly ICaseService caseService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;
        private readonly CookieOptions cookieOptions;

        public CartController(
            ICaseService caseService,
            UserManager<ApplicationUser> userManager,
            ICartService cartService)
        {
            this.caseService = caseService;
            this.userManager = userManager;
            this.cartService = cartService;
            this.cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(CookieExpiresInDays),
                Secure = true,
            };
        }

        [HttpPost]
        [Route(nameof(AddItem))]
        public async Task<IActionResult> AddItem(string id)
        {
            var caseExists = this.caseService.Exists(id);
            var userId = this.userManager.GetUserId(this.User);
            if (caseExists)
            {
                var cookie = this.Request.Cookies[CookieName];
                if (userId != null)
                {
                    if (cookie != null)
                    {
                        var caseIds = cookie.Split(CookieDelimeter);
                        foreach (var caseId in caseIds)
                        {
                            await this.cartService.AddItemByIdAndUserIdAsync(caseId, userId);
                        }
                    }
                    else
                    {
                        await this.cartService.AddItemByIdAndUserIdAsync(id, userId);
                    }

                    // deletes cookie if user is logged in
                    this.Response.Cookies.Append(CookieName, string.Empty, new CookieOptions
                    {
                        SameSite = SameSiteMode.None,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddDays(-1),
                    });
                    return this.Ok();
                }
                else
                {
                    if (cookie == null)
                    {
                        this.Response.Cookies.Append(CookieName, id, this.cookieOptions);
                    }
                    else
                    {
                        var newCookieValue = string.Join(CookieDelimeter, cookie, id);
                        this.Response.Cookies.Append(CookieName, newCookieValue, this.cookieOptions);
                    }

                    return this.Ok();
                }
            }

            return this.NotFound();
        }

        [HttpGet]
        [Route(nameof(Count))]
        public int Count()
        {
            var userId = this.userManager.GetUserId(this.User);
            var count = 0;
            if (userId == null)
            {
                count = this.Request.Cookies[CookieName] == null ? count :
                this.Request.Cookies[CookieName]
                .Split(CookieDelimeter)
                .Count();
            }
            else
            {
                count = this.cartService.GetItemsCountByUserId(userId);
            }

            return count;
        }
    }
}
