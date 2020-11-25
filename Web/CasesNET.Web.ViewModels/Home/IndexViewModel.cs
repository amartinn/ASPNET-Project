namespace CasesNET.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<CategoryViewModel> MostSoldCategories { get; set; }

        public IEnumerable<CaseViewModel> BestSellersCases { get; set; }

        public IEnumerable<ManufacturerListingViewModel> Manufacturers { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
