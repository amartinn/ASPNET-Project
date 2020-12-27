namespace CasesNET.Web.ViewModels.Categories
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CategorySelectListModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
