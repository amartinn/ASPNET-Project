namespace CasesNET.Services.Data.Tests.FakeModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class FakeDateTime
    {
        public static DateTime Now() => new DateTime(2020, 1, 1);

        public static DateTime Now(int day) => new DateTime(2020, 1, day);
    }
}
