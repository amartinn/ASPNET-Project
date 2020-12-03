namespace CasesNET.Web.Areas.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("api")]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
