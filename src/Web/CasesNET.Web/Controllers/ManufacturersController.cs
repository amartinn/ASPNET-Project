namespace CasesNET.Web.Controllers
{
    using CasesNET.Services.Data;
    using CasesNET.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Mvc;

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
            return this.View(viewModel);
        }
    }
}
