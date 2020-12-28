namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Administration.Cases;
    using CasesNET.Web.ViewModels.Categories;
    using CasesNET.Web.ViewModels.Devices;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CasesController : AdministrationController
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICaseService caseService;
        private readonly ICategoryService categoryService;
        private readonly IDeviceService deviceService;

        public CasesController(
            IWebHostEnvironment webHostEnvironment,
            ICaseService caseService,
            ICategoryService categoryService,
            IDeviceService deviceService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.caseService = caseService;
            this.categoryService = categoryService;
            this.deviceService = deviceService;
        }

        // GET: Administration/Cases
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new CaseListingViewModel
            {
                Cases = this.caseService.GetAll<CaseViewModel>(),
            };
            return this.View(viewModel);
        }

        // GET: Administration/Cases/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCaseInputModel
            {
                Categories = this.GetCategoriesAsSelectList(),
                Devices = this.GetDevicesAsSelectList(),
            };
            return this.View(model);
        }

        // POST: Administration/Cases/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCaseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.GetCategoriesAsSelectList();
                model.Devices = this.GetDevicesAsSelectList();
                var message = string.Join(" | ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage));
                return this.Json(new {Status = "Error!", Message = message });
            }

            var path = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/Cases");
            await this.caseService.CreateAsync(model, path);
            return this.Json(new {Status = "Success", Url = this.Url.Action(nameof(this.Index)) });
        }

        // GET: Administration/Cases/Edit/5
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var @case = this.caseService.GetById<CaseEditInputModel>(id);
            @case.Categories = this.GetCategoriesAsSelectList();
            @case.Devices = this.GetDevicesAsSelectList();
            if (@case == null)
            {
                return this.NotFound();
            }

            return this.View(@case);
        }

        // POST: Administration/Cases/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(CaseEditInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.caseService.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CaseExists(model.Id))
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

            model.Categories = this.GetCategoriesAsSelectList();
            model.Devices = this.GetDevicesAsSelectList();
            return this.View(model);
        }

        // GET: Administration/Cases/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var @case = this.caseService.GetById<CaseDeleteViewModel>(id);
            if (@case == null)
            {
                return this.NotFound();
            }

            return this.View(@case);
        }

        // POST: Administration/Cases/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.caseService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CaseExists(string id)
        {
            return this.caseService.GetAll<CaseViewModel>().Any(e => e.Id == id);
        }

        private SelectList GetCategoriesAsSelectList()
            => new SelectList(this.categoryService.GetAll<CategorySelectListModel>(), "Id", "Name");

        private SelectList GetDevicesAsSelectList()
            => new SelectList(this.deviceService.GetAll<DeviceSelectListModel>(), "Id", "Name");
    }
}
