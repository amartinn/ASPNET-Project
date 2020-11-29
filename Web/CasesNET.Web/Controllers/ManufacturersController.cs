using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasesNET.Services.Data;
using CasesNET.Web.ViewModels.Manufacturers;
using Microsoft.AspNetCore.Mvc;

namespace CasesNET.Web.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService manufacturerservice;

        public ManufacturersController(IManufacturerService manufacturerservice)
        {
            this.manufacturerservice = manufacturerservice;
        }

        public IActionResult All()
        {
            var viewModel = new ManufacturerListingViewModel
            {
                Manufacturers = this.manufacturerservice
                .GetAll<ManufacturerViewModel>(),
            };
            return View(viewModel);
        }
    }
}
