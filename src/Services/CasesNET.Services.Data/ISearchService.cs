namespace CasesNET.Services.Data
{
    using System.Collections.Generic;

    using static CasesNET.Common.GlobalConstants.Paging;

    public interface ISearchService
    {
        IEnumerable<T> GetAllCasesBySearchTerm<T>(string term, int page = 1, int itemsPerPage = ItemsPerPage);

        int GetCountBySearchTerm(string term);
    }
}
