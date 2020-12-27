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
    using CasesNET.Web.ViewModels.Administration.Manufacturers;
    using CasesNET.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class ManufacturersController : AdministrationController
    {
        private readonly IManufacturerService manufacturerService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagePath;

        public ManufacturersController(IManufacturerService manufacturerService, IWebHostEnvironment webHostEnvironment)
        {
            this.manufacturerService = manufacturerService;
            this.webHostEnvironment = webHostEnvironment;
            this.imagePath = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/Manufacturers");
        }

        // GET: Administration/Manufacturers
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ManufacturerListingViewModel
            {
                Manufacturers = this.manufacturerService.GetAll<ManufacturerViewModel>(),
            };
            return this.View(model);
        }

        // GET: Administration/Manufacturers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Manufacturers/Create
        [HttpPost]
        public async Task<IActionResult> Create(ManufacturerCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.manufacturerService.CreateAsync(model, this.imagePath);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: Administration/Manufacturers/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var manufacturer = this.manufacturerService.GetById<ManufacturerEditInputModel>(id);
            if (manufacturer == null)
            {
                return this.NotFound();
            }

            return this.View(manufacturer);
        }

        // POST: Administration/Manufacturers/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(ManufacturerEditInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.manufacturerService.UpdateAsync(model, this.imagePath);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ManufacturerExists(model.Id))
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

        // GET: Administration/Manufacturers/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var manufacturer = this.manufacturerService.GetById<ManufacturerViewModel>(id);
            if (manufacturer == null)
            {
                return this.NotFound();
            }

            return this.View(manufacturer);
        }

        // POST: Administration/Manufacturers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.manufacturerService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ManufacturerExists(string id)
            => this.manufacturerService.GetAll<ManufacturerViewModel>().Any(x => x.Id == id);
    }
}
