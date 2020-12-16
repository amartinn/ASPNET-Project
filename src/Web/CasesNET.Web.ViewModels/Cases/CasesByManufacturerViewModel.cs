namespace CasesNET.Web.ViewModels.Cases
{
    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class CasesByManufacturerViewModel : PagingViewModel
    {
        public string ManufacturerName { get; set; }

        public string ManufacturerId { get; set; }

        public IEnumerable<CaseViewModel> Cases { get; set; }
    }
}
