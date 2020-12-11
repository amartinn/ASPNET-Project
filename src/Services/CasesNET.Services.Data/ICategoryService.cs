namespace CasesNET.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoryService
    {
       IEnumerable<T> GetMostSold<T>(int count = 4);

       IEnumerable<T> GetAll<T>();

        string GetNameById(string id);
    }
}
