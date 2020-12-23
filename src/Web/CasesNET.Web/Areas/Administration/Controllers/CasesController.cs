namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data;
    using CasesNET.Data.Models;
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Administration.Cases;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CasesController : AdministrationController
    {
#pragma warning disable SA1309 // Field names should not begin with underscore
        private readonly ApplicationDbContext _context;
#pragma warning restore SA1309 // Field names should not begin with underscore
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICaseService caseService;

        public CasesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ICaseService caseService)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.caseService = caseService;
        }

        // GET: Administration/Cases
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Cases = this.caseService.GetAll<CaseViewModel>(),
            };
            return this.View(viewModel);
        }

        // GET: Administration/Cases/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name");
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Cases/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCaseInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var path = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/Cases");
                await this.caseService.CreateAsync(model, path);
                return this.Json(new { redirectToUrl = this.Url.Action(nameof(this.Index)) });
            }

            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name", model.CategoryId);
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Name", model.DeviceId);
            return this.View(model);
        }

        // GET: Administration/Cases/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name");
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Name");
            var @case = this.caseService.GetById<EditViewModel>(id);
            if (@case == null)
            {
                return this.NotFound();
            }

            return this.View(@case);
        }

        // POST: Administration/Cases/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
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

            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name", model.DeviceId);
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Name", model.DeviceId);
            return this.View(model);
        }

        // GET: Administration/Cases/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var @case = this.caseService.GetById<CaseDeleteModel>(id);
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
    }
}
