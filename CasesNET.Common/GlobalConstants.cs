namespace CasesNET.Common
{
    using System;

    using Microsoft.AspNetCore.Http;

    public class GlobalConstants
    {
        public class Domain
        {
            public const string SystemName = "CasesNET";

            public const string AdministratorRoleName = "Administrator";
        }

        public class ShoppingCart
        {
            public const string CookieName = "_cart";
            public const string CookieDelimeter = ";";
            public const int CookieExpiresInDays = 10;
        }
    }
}
