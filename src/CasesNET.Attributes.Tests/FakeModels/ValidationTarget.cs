namespace CasesNET.Attributes.Tests.FakeModels
{
    internal class ValidationTarget
    {
        [Country]
        public string CountryName { get; set; }

        [City]
        public string CityName { get; set; }
    }
}
