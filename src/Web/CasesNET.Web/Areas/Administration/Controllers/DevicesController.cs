namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Administration.Devices;
    using CasesNET.Web.ViewModels.Administration.Manufacturers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class DevicesController : AdministrationController
    {
        private readonly IDeviceService deviceService;
        private readonly IManufacturerService manufacturerService;

        public DevicesController(IDeviceService deviceService, IManufacturerService manufacturerService)
        {
            this.deviceService = deviceService;
            this.manufacturerService = manufacturerService;
        }

        // GET: Administration/Devices
        public IActionResult Index()
        {
            var model = this.deviceService.GetAll<DeviceViewModel>();
            return this.View(model);
        }

        // GET: Administration/Devices/Create
        public IActionResult Create()
        {
            var model = new DeviceCreateInputModel
            {
                Manufacturers = this.GetManufacturersAsSelectList(),
            };

            return this.View();
        }

        // POST: Administration/Devices/Create
        [HttpPost]
        public async Task<IActionResult> Create(DeviceCreateInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.deviceService.CreateAsync(model);
                return this.RedirectToAction(nameof(this.Index));
            }

            model.Manufacturers = this.GetManufacturersAsSelectList();
            return this.View(model);
        }

        // GET: Administration/Devices/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var device = this.deviceService.GetById<DeviceEditInputModel>(id);
            if (device == null)
            {
                return this.NotFound();
            }

            device.Manufacturers = this.GetManufacturersAsSelectList();
            return this.View(device);
        }

        // POST: Administration/Devices/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(DeviceEditInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                   await this.deviceService.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DeviceExists(model.Id))
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

            model.Manufacturers = this.GetManufacturersAsSelectList();
            return this.View(model);
        }

        // GET: Administration/Devices/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var device = this.deviceService.GetById<DeviceViewModel>(id);
            if (device == null)
            {
                return this.NotFound();
            }

            return this.View(device);
        }

        // POST: Administration/Devices/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.deviceService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DeviceExists(string id)
            => this.deviceService.GetAll<DeviceViewModel>().Any(x => x.Id == id);

        private SelectList GetManufacturersAsSelectList()
            => new SelectList(this.manufacturerService.GetAll<ManufacturerSelectListModel>(), "Id", "Name");
    }
}
