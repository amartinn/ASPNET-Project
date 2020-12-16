namespace CasesNET.Web.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Categories;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult All()
        {
            var viewModel = new CategoryListingModel
            {
                Categories = this.categoryService
                .GetAll<CategoryViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
