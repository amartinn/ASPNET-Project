namespace CasesNET.Services.Data.Tests.FakeModels
{
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class FakeCategoryModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
