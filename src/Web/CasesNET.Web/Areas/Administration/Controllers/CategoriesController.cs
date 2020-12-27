namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Administration.Categories;
    using CasesNET.Web.ViewModels.Categories;
    using CasesNET.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoriesController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Administration/Categories
        [HttpGet]
        public IActionResult Index()
        {
            var model = new CategoryListingModel
            {
                Categories = this.categoryService.GetAll<CategoryViewModel>(),
            };
            return this.View(model);
        }

        // GET: Administration/Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {

                await this.categoryService.CreateAsync(model,this.webHostEnvironment.WebRootPath);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: Administration/Categories/Edit/5
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoryService.GetById<CategoryEditInputModel>(id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var path = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/Categories");
                    await this.categoryService.UpdateAsync(model, path);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(model.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: Administration/Categories/Delete/5
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoryService.GetById<CategoryViewModel>(id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Administration/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.categoryService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CategoryExists(string id)
            => this.categoryService.GetAll<CategoryViewModel>().Any(x => x.Id == id);
    }
}
