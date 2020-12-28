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
    using CasesNET.Web.ViewModels.Administration.Orders;
    using CasesNET.Web.ViewModels.Checkout;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<Cart> cartRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public OrderService(
            IRepository<Order> orderRepository,
            IDeletableEntityRepository<Cart> cartRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateAsync(CheckoutInputModel model)
        {
            var order = new Order
            {
                CartId = model.CartId,
                OrderStatus = OrderStatus.Pending,
                OrderedById = model.UserId,
            };
            var user = this.userRepository.All()
                .FirstOrDefault(x => x.Id == model.UserId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Adress = model.Adress;
            user.PhoneNumber = model.PhoneNumber;
            var cart = this.cartRepository.All().FirstOrDefault(x => x.Id == model.CartId);
            cart.OrderId = order.Id;
            await this.orderRepository.AddAsync(order);
            this.cartRepository.Update(cart);
            this.userRepository.Update(user);

            await this.userRepository.SaveChangesAsync();
            await this.cartRepository.SaveChangesAsync();
            await this.orderRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
            => this.orderRepository
            .AllAsNoTracking()
            .To<T>();

        public IEnumerable<T> GetAllByUserId<T>(string userId)
            => this.orderRepository.AllAsNoTracking()
            .Where(x => x.OrderedById == userId)
            .To<T>()
            .ToList();

        public IEnumerable<T> GetAllItemsByOrderId<T>(int orderId)
        {
            var cartId = this.orderRepository
             .AllAsNoTracking()
             .Where(x => x.Id == orderId)
             .FirstOrDefault()
             .CartId;

            var cartItems = this.cartRepository
                .AllWithDeleted()
                .FirstOrDefault(x => x.Id == cartId)
                .Items
                .AsQueryable()
                .To<T>();

            return cartItems;
        }

        public T GetById<T>(int id)
              => this.orderRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();

        public async Task UpdateAsync(OrderEditInputModel model)
        {
            var order = this.orderRepository
                .All()
                .FirstOrDefault(x => x.Id == model.Id);
            order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), model.OrderStatus);
            order.OrderedBy.FirstName = model.FirstName;
            order.OrderedBy.LastName = model.LastName;
            order.OrderedBy.PhoneNumber = model.PhoneNumber;
            order.OrderedBy.Email = model.Email;
            order.OrderedBy.Adress = model.Adress;
            order.OrderedBy.City = model.City;
            order.OrderedBy.Country = model.Country;

            this.orderRepository.Update(order);
            this.userRepository.Update(order.OrderedBy);
            await this.orderRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }
    }
}
