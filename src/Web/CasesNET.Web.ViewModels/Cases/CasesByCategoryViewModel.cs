namespace CasesNET.Web.ViewModels.Cases
{
    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class CasesByCategoryViewModel : PagingViewModel
    {
        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }

        public override string FriendlyUrlName => this.CategoryName;

        public override string FriendlyUrlPrefix => "Categories";
    }
}
