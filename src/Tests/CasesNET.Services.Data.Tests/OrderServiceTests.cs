namespace CasesNET.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Data.Models.Enum;
    using CasesNET.Services.Data.Tests.FakeModels;
    using CasesNET.Services.Mapping;
    using CasesNET.Web.ViewModels.Checkout;
    using Moq;
    using Xunit;

    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Order>> orderRepository;
        private readonly Mock<IRepository<Cart>> cartRepository;
        private readonly string userId = "userId";
        private readonly string cartId = "cartId";

        public OrderServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeOrderModel).GetTypeInfo().Assembly);
            this.orderRepository = new Mock<IRepository<Order>>();
            this.cartRepository = new Mock<IRepository<Cart>>();
        }

        [Fact]
        public async Task CreateAsyncMethodShouldCreateOrder()
        {
            // Arrange
            var fakeOrders = new List<Order>();
            var fakeCarts = new List<Cart>
            {
                new Cart
                {
                    Id = this.cartId,
                    UserId = this.userId,
                },
            };
            this.orderRepository.Setup(s => s.AddAsync(It.IsAny<Order>()))
                .Callback((Order order) =>
                {
                    fakeOrders.Add(order);
                });
            this.cartRepository.Setup(s => s.All())
                .Returns(fakeCarts.AsQueryable());

            var checkoutModel = new CheckoutInputModel
            {
                CartId = this.cartId,
                UserId = this.userId,
            };
            var service = new OrderService(this.orderRepository.Object, this.cartRepository.Object);

            // Act
            await service.CreateAsync(checkoutModel);

            // Assert
            var order = fakeOrders.First();
            var orderId = order.Id;
            var cartOrderId = fakeCarts.First().OrderId;
            Assert.NotEmpty(fakeOrders);
            Assert.Equal(orderId, cartOrderId);
            Assert.Equal(OrderStatus.Pending, order.OrderStatus);
        }

        [Fact]
        public void GetAllByUserIdMethodShouldReturnTheCorrectOrders()
        {
            // Arrange
            const int expected = 2;
            var fakeOrders = new List<Order>
            {
                new Order
                {
                    OrderedById = this.userId,
                },
                new Order
                {
                    OrderedById = this.userId,
                },
                new Order
                {
                    OrderedById = this.userId + "2",
                },
            };
            this.orderRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeOrders.AsQueryable());

            var service = new OrderService(this.orderRepository.Object, null);

            // Act
            var items = service.GetAllByUserId<FakeOrderModel>(this.userId);

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
            Assert.True(items.All(x => x.OrderedById == this.userId));
        }
    }
}
