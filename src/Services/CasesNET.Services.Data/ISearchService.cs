namespace CasesNET.Services.Data
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        IEnumerable<T> GetAllCasesBySearchTerm<T>(string term, int page = 1, int itemsPerPage = 12);

        int GetCountBySearchTerm(string term);
    }
}
