using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasesNET.Data;
using CasesNET.Data.Models;
using CasesNET.Web.ViewModels.Administration.Cases;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CasesNET.Services.Data;

namespace CasesNET.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICaseService caseService;

        public CasesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ICaseService caseService)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.caseService = caseService;
        }

        // GET: Administration/Cases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this._context.Cases.Include(c => c.CartItem).Include(c => c.Category).Include(c => c.Device).Include(c => c.Image);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Cases/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var @case = await this._context.Cases
                .Include(c => c.CartItem)
                .Include(c => c.Category)
                .Include(c => c.Device)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@case == null)
            {
                return this.NotFound();
            }

            return this.View(@case);
        }

        // GET: Administration/Cases/Create
        public IActionResult Create()
        {

            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name");
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(CreateCaseInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var path = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/Cases");
                await this.caseService.CreateAsync(model, path);
                return this.Json(new { redirectToUrl = this.Url.Action(nameof(this.Index)) });
            }

            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Id", model.CategoryId);
            this.ViewData["DeviceId"] = new SelectList(this._context.Devices, "Id", "Id", model.DeviceId);
            return this.View(model);
        }

        // GET: Administration/Cases/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            ViewData["CartItemId"] = new SelectList(_context.CartItems, "Id", "Id", @case.CartItemId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", @case.CategoryId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Id", @case.DeviceId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", @case.ImageId);
            return View(@case);
        }

        // POST: Administration/Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,ImageId,DeviceId,CategoryId,Price,Description,CartItemId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Case @case)
        {
            if (id != @case.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartItemId"] = new SelectList(_context.CartItems, "Id", "Id", @case.CartItemId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", @case.CategoryId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Id", @case.DeviceId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", @case.ImageId);
            return View(@case);
        }

        // GET: Administration/Cases/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .Include(c => c.CartItem)
                .Include(c => c.Category)
                .Include(c => c.Device)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Administration/Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @case = await _context.Cases.FindAsync(id);
            _context.Cases.Remove(@case);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(string id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }
    }
}
