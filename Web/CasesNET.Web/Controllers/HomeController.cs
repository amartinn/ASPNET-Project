namespace CasesNET.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels;
    using CasesNET.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICaseService caseService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IRepository<Device> deviceRepository;

        public HomeController(
            ICaseService caseService,
            ICategoryService categoryService,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Device> deviceRepository)
        {
            this.caseService = caseService;
            this.categoryService = categoryService;
            this.manufacturerRepository = manufacturerRepository;
            this.deviceRepository = deviceRepository;
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
