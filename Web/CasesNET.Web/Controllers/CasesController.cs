namespace CasesNET.Web.Controllers
{
    using System.Linq;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cases;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : Controller
    {
        private readonly ICaseService caseService;

        public CasesController(ICaseService caseService)
        {
            this.caseService = caseService;
        }

        public IActionResult ByCategory(string id)
        {
            var cases = this.caseService.GetAllByCategory<CaseViewModel>(id);
            var viewModel = new CasesByCategoryViewModel
            {
                Cases = cases,
                CategoryName = cases.FirstOrDefault().CategoryName,
            };
            return this.View(viewModel);
        }

        public IActionResult Latest()
        {
            var model = new CaseListingViewModel
            {
                Cases = this.caseService.GetLatest<CaseViewModel>(),

            };
            return this.View(model);
        }
    }
}
