namespace CasesNET.Web.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Search;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public IActionResult ByTerm(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return this.Redirect(url: "/Home");
            }

            var casesMatchingSearchTerm =
                this.searchService.GetAllCasesBySearchTerm<CaseViewModel>(term);
            var viewModel = new SearchViewModel
            {
                SearchTerm = term,
                Cases = casesMatchingSearchTerm,
            };
            return this.View(viewModel);
        }
    }
}
