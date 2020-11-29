namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICaseService
    {
        T GetById<T>(string id);

        bool Exists(string id);

        Task CreateAsync();

        int Count();

        IEnumerable<T> GetBestSellers<T>(int count = 4);

        IEnumerable<T> GetAllByCategory<T>(string categoryId);

        IEnumerable<T> GetLatest<T>(int count = 12);
    }
}
