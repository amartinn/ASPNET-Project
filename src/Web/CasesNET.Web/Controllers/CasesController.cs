namespace CasesNET.Web.Controllers
{
    using System.Linq;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cases;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : Controller
    {
        private const int ItemsPerPage = 12;

        private readonly ICaseService caseService;

        public CasesController(ICaseService caseService)
        {
            this.caseService = caseService;
        }

        public IActionResult ByCategory(string id, int page = 1)
        {
            var cases = this.caseService.GetAllByCategory<CaseViewModel>(id, page, ItemsPerPage);

            var viewModel = new CasesByCategoryViewModel
            {
                PageNumber = page,
                ItemsPerPage = ItemsPerPage,
                CasesCount = this.caseService.CountByCategory(id),
                Cases = cases,
                CategoryName = cases.FirstOrDefault().CategoryName,
                CategoryId = cases.FirstOrDefault().CategoryId,
            };
            this.ViewData["id"] = viewModel.CategoryId;
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

        public IActionResult ByManufacturer(string id, int page = 1)
        {
            var casesByManufacturer = this.caseService.GetByManufacturerId<CaseViewModel>(id);

            var viewModel = new CasesByManufacturerViewModel
            {
                PageNumber = page,
                ItemsPerPage = ItemsPerPage,
                CasesCount = this.caseService.CountByManufacturer(id),
                ManufacturerName = casesByManufacturer.First().ManufacturerName,
                ManufacturerId = casesByManufacturer.First().ManufacturerId,
                Cases = casesByManufacturer,
            };
            this.ViewData["id"] = viewModel.ManufacturerId;
            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.caseService.GetById<CaseDetailsViewModel>(id);
            viewModel.RelatedCases = this.caseService.GetAllByCategory<CaseViewModel>(viewModel.CategoryId).Take(4);
            return this.View(viewModel);
        }
    }
}
