namespace CasesNET.Web.Controllers
{
    using System;
    using System.Linq;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Cases;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : Controller
    {
        private const int ItemsPerPage = 12;

        private readonly ICaseService caseService;
        private readonly IManufacturerService manufacturerService;
        private readonly ICategoryService categoryService;

        public CasesController(ICaseService caseService, IManufacturerService manufacturerService, ICategoryService categoryService)
        {
            this.caseService = caseService;
            this.manufacturerService = manufacturerService;
            this.categoryService = categoryService;
        }

        public IActionResult ByCategory(string id, int page = 1)
        {
            var categoryName = this.categoryService.GetNameById(id);
            try
            {
                var cases = this.caseService.GetAllByCategory<CaseViewModel>(id, page, ItemsPerPage);

                var viewModel = new CasesByCategoryViewModel
                {
                    PageNumber = page,
                    ItemsPerPage = ItemsPerPage,
                    CasesCount = this.caseService.GetItemsCountByCategoryId(id),
                    Cases = cases,
                    CategoryName = categoryName,
                    CategoryId = id,
                };
                this.ViewData["id"] = viewModel.CategoryId;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var model = new CasesByCategoryViewModel
                {
                    CasesCount = this.caseService.GetCountByManufacturer(id),
                    CategoryName = categoryName,
                };
                return this.View(model);
            }
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
            var manufacturerName = this.manufacturerService.GetNameById(id);
            try
            {
                var casesByManufacturer = this.caseService.GetByManufacturerId<CaseViewModel>(id);

                var viewModel = new CasesByManufacturerViewModel
                {
                    PageNumber = page,
                    ItemsPerPage = ItemsPerPage,
                    CasesCount = this.caseService.GetCountByManufacturer(id),
                    ManufacturerName = manufacturerName,
                    ManufacturerId = casesByManufacturer.First().ManufacturerId,
                    Cases = casesByManufacturer,
                };
                this.ViewData["id"] = viewModel.ManufacturerId;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var model = new CasesByManufacturerViewModel
                {
                    CasesCount = this.caseService.GetCountByManufacturer(id),
                    ManufacturerName = manufacturerName,
                };
                return this.View(model);
            }
        }

        public IActionResult Details(string caseId)
        {
            var viewModel = this.caseService.GetById<CaseDetailsViewModel>(caseId);
            viewModel.RelatedCases = this.caseService.GetAllByCategory<CaseViewModel>(viewModel.CategoryId).Take(4);
            return this.View(viewModel);
        }
    }
}
