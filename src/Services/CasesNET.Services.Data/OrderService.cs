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
        private readonly IRepository<Cart> cartRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<Cart> cartRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
        }


        public async Task CreateAsync(CheckoutInputModel model)
        {
            var order = new Order
            {
                CartId = model.CartId,
                OrderStatus = OrderStatus.Pending,
                OrderedById = model.UserId,
            };
            var cart = this.cartRepository.All().FirstOrDefault(x => x.Id == model.CartId);
            cart.OrderId = order.Id;
            await this.orderRepository.AddAsync(order);
            this.cartRepository.Update(cart);
            await this.cartRepository.SaveChangesAsync();
            await this.orderRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
            => this.orderRepository.AllAsNoTracking()
            .Where(x => x.OrderedById == userId)
            .To<T>()
            .ToList();
    }
}
