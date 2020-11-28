namespace CasesNET.Web.ViewModels.Category
{

    using System.Collections.Generic;

    using CasesNET.Web.ViewModels.Shared;

    public class CategoryListingModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
