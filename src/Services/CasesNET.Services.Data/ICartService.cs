namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task AddItemByIdAndUserIdAsync(string caseId, string userId);

        Task RemoveItemByIdAndUserIdAsync(string caseId, string userId);

        IEnumerable<T> GetAllItemsByUserId<T>(string userId);

        int GetItemsCountByUserId(string userId);
    }
}
