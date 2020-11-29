namespace CasesNET.Web.ViewModels.Cases
{
    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class CasesByManufacturerViewModel
    {
        public string ManufacturerName { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }
    }
}
