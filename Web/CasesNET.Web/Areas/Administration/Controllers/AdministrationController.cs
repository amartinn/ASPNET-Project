namespace CasesNET.Web.Areas.Administration.Controllers
{
    using CasesNET.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CasesNET.Common.GlobalConstants.Domain;

    [Authorize(Roles = AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
