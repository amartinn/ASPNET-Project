namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Data.Models.Enum;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Checkout;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task CreateAsync(CheckoutInputModel model)
        {
            var order = new Order
            {
                CartId = model.CartId,
                OrderStatus = OrderStatus.Pending,
                OrderedById = model.UserId,
            };
            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
            => this.orderRepository.AllAsNoTracking()
            .Where(x => x.OrderedById == userId)
            .To<T>()
            .ToList();
    }
}
