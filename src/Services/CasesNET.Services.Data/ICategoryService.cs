namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Administration.Categories;

    public interface ICategoryService
    {
       IEnumerable<T> GetMostSold<T>(int count = 4);

       IEnumerable<T> GetAll<T>();

       string GetNameById(string id);

       T GetById<T>(string id);

       Task CreateAsync(CategoryCreateInputModel model, string imagePath);

       Task UpdateAsync(CategoryEditInputModel model, string imagePath);

       Task DeleteByIdAsync(string id);
    }
}
