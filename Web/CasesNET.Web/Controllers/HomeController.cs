namespace CasesNET.Web.Controllers
{
    using System.Diagnostics;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels;
    using CasesNET.Web.ViewModels.Home;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICaseService caseService;
        private readonly ICategoryService categoryService;

        public HomeController(
            ICaseService caseService,
            ICategoryService categoryService)
        {
            this.caseService = caseService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                BestSellersCases = this.caseService.GetBestSellers<CaseViewModel>(),
                MostSoldCategories = this.categoryService.GetMostSold<CategoryViewModel>(),
            };
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
