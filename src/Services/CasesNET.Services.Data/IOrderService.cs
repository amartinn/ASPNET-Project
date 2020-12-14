namespace CasesNET.Services.Data
{
    using System.Threading.Tasks;

    using CasesNET.Web.ViewModels.Checkout;

    public interface IOrderService
    {
        Task CreateAsync(CheckoutInputModel model);
    }
}
