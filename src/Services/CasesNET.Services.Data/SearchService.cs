namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class SearchService : ISearchService
    {
        private readonly IRepository<Case> caseRepository;

        public SearchService(IRepository<Case> caseRepository)
        {
            this.caseRepository = caseRepository;
        }

        public int GetCountBySearchTerm(string term)
            => this.caseRepository
            .All()
            .ToList()
            .Where(x =>
            x.Category.Name.ToLower().Contains(term.ToLower()) ||
            x.Device.Name.ToLower().Contains(term.ToLower()) ||
            x.Device.Manufactorer.Name.ToLower().Contains(term.ToLower()) ||
            x.Description.ToLower().Contains(term.ToLower()))
            .Count();

        public IEnumerable<T> GetAllCasesBySearchTerm<T>(string term, int page = 1, int itemsPerPage = 12)
            => this.caseRepository
            .All()
            .ToList()
            .Where(x =>
            x.Category.Name.ToLower().Contains(term.ToLower()) ||
            x.Device.Name.ToLower().Contains(term.ToLower()) ||
            x.Device.Manufactorer.Name.ToLower().Contains(term.ToLower()) ||
            x.Description.ToLower().Contains(term.ToLower()))
            .AsQueryable()
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .To<T>()
            .ToList();
    }
}
