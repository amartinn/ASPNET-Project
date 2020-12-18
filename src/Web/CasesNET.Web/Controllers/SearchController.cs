namespace CasesNET.Web.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Search;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    using static CasesNET.Common.GlobalConstants.Paging;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly ICaseService caseService;

        public SearchController(ISearchService searchService, ICaseService caseService)
        {
            this.searchService = searchService;
            this.caseService = caseService;
        }

        [HttpGet]
        public IActionResult ByTerm(string term, int page = 1)
        {
            var tempdataValue = this.TempData["term"].ToString();
            term ??= tempdataValue;
            var casesMatchingSearchTerm =
                this.searchService.GetAllCasesBySearchTerm<CaseViewModel>(term, page);
            var viewModel = new SearchViewModel
            {
                ItemsPerPage = ItemsPerPage,
                CasesCount = this.searchService.GetCountBySearchTerm(term),
                PageNumber = page,
                SearchTerm = term,
                Cases = casesMatchingSearchTerm,
                BestSellerCases = this.caseService.GetBestSellers<CaseViewModel>(8),
            };
            this.TempData["term"] = term;
            this.ViewData["id"] = term;
            return this.View(viewModel);
        }
    }
}
