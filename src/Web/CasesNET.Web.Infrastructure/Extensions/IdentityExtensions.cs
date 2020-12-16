namespace CasesNET.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static CasesNET.Common.GlobalConstants.Domain;

    public static class IdentityExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
