namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Administration.Orders;
    using CasesNET.Web.ViewModels.Checkout;

    public interface IOrderService
    {
        Task CreateAsync(CheckoutInputModel model);

        IEnumerable<T> GetAllByUserId<T>(string id);

        T GetById<T>(int id);

        Task UpdateAsync(OrderEditInputModel model);

        IEnumerable<T> GetAll<T>();
    }
}
