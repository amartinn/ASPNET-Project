namespace CasesNET.Web.ViewModels.Cases
{
    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class CasesByCategoryViewModel
    {
        public string CategoryName { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }
    }
}
