namespace CasesNET.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class SearchViewModel : PagingViewModel
    {
        public string SearchTerm { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }

        public IEnumerable<CaseViewModel> BestSellerCases { get; set; }
    }
}
