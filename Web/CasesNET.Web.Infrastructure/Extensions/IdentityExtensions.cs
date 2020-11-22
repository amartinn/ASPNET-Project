namespace CasesNET.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    using CasesNET.Common;

    public static class IdentityExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(GlobalConstants.AdministratorRoleName);
    }
}
