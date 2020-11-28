namespace CasesNET.Services.Data
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        IEnumerable<T> GetAllCasesBySearchTerm<T>(string term);
    }
}
