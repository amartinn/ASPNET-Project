namespace CasesNET.Web.ViewModels.Search
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CasesNET.Web.ViewModels.Shared;

    public class SearchViewModel : PagingViewModel
    {
        [Required]
        [MinLength(2)]
        public string SearchTerm { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }
    }
}
