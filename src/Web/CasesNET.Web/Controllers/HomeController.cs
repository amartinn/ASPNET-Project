namespace CasesNET.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels;
    using CasesNET.Web.ViewModels.Home;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
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
            const int totalItems = 4;
            var mostSoldCategories = this.categoryService.GetMostSold<CategoryViewModel>(totalItems).ToList();

            // if there arent any items sold for at least 4 categories, we add random categories up to 4.
            // if there is sold category we make sure that there are no duplicates.
            var itemsToBeAdded = totalItems - mostSoldCategories.Count();
            var categories = this.categoryService.GetAll<CategoryViewModel>()
                .Where(x => mostSoldCategories.Select(y => y.Id).Contains(x.Id) == false)
                .OrderBy(_ => Guid.NewGuid())
                .Take(itemsToBeAdded);

            mostSoldCategories.AddRange(categories);
            var model = new IndexViewModel
            {
                MostSoldCategories = mostSoldCategories,
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
