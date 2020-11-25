namespace CasesNET.Web.Areas.Api.Controllers
{
    using System;
    using System.Linq;

    using CasesNET.Services.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using static CasesNET.Common.GlobalConstants.ShoppingCart;

    public class CartController : ApiController
    {
        private readonly ICaseService caseService;
        private readonly CookieOptions cookieOptions;

        public CartController(ICaseService caseService)
        {
            this.caseService = caseService;
            this.cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(CookieExpiresInDays),
                Secure = true,
            };
        }

        [HttpPost]
        [Route(nameof(AddItem))]
        public IActionResult AddItem(string id)
        {
            var exists = this.caseService.Exists(id);
            if (exists)
            {
                var cookie = this.Request.Cookies[CookieName];
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

            return this.NotFound();
        }

        [HttpGet]
        [Route(nameof(Count))]
        public int Count()
            => this.Request.Cookies[CookieName] == null ? 0 :
            this.Request.Cookies[CookieName]
            .Split(CookieDelimeter)
            .Count();
    }
}
