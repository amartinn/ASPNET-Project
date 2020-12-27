namespace CasesNET.Web.ViewModels.Administration.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CaseListingViewModel
    {
       public IEnumerable<CaseViewModel> Cases { get; set; }
    }
}
