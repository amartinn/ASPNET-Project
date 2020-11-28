namespace CasesNET.Web.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Category;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(
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
