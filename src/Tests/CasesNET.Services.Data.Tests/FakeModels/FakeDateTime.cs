namespace CasesNET.Services.Data.Tests.FakeModels
{
    using System;

    public static class FakeDateTime
    {
        public static DateTime Now() => new DateTime(2020, 1, 1);

        public static DateTime Now(int day) => new DateTime(2020, 1, day);
    }
}
