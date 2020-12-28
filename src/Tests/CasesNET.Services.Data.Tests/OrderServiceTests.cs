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
    using CasesNET.Web.ViewModels.Administration.Orders;
    using CasesNET.Web.ViewModels.Checkout;
    using DeepEqual.Syntax;
    using Moq;
    using Xunit;

    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Order>> orderRepository;
        private readonly Mock<IRepository<Cart>> cartRepository;
        private readonly string userId = "userId";
        private readonly string cartId = "cartId";
        private readonly int orderId = 10;

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
            var fakeUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = this.userId,
                },
            };
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
            var userRepository = new Mock<IRepository<ApplicationUser>>();
            userRepository.Setup(s => s.All())
                .Returns(fakeUsers.AsQueryable());
            var checkoutModel = new CheckoutInputModel
            {
                CartId = this.cartId,
                UserId = this.userId,
            };
            var service = new OrderService(
                this.orderRepository.Object,
                this.cartRepository.Object,
                userRepository.Object);

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

            var service = new OrderService(this.orderRepository.Object, null, null);

            // Act
            var items = service.GetAllByUserId<FakeOrderModel>(this.userId);

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
            Assert.True(items.All(x => x.OrderedById == this.userId));
        }

        [Fact]
        public void GetAllMethodShouldReturnTheCorrectOrders()
        {
            // Arrange
            const int expected = 3;
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
                    OrderedById = this.userId,
                },
            };
            this.orderRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeOrders.AsQueryable());

            var service = new OrderService(this.orderRepository.Object, null, null);

            // Act
            var items = service.GetAll<FakeOrderModel>();

            // Assert
            var actual = items.Count();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByIdMethodShouldReturnTheCorrectOrder()
        {
            // Arrange
            var expected = this.orderId;
            var fakeCases = new List<Order>
            {
                new Order
                {
                   Id = this.orderId,
                   OrderedById = "orderedId",
                },
                new Order
                {
                    Id = this.orderId + 1,
                    OrderedById = "orderedId",
                },
            };

            this.orderRepository.Setup(s => s.AllAsNoTracking())
                .Returns(fakeCases.AsQueryable());
            var service = new OrderService(this.orderRepository.Object, null, null);

            // Act
            var order = service.GetById<FakeOrderModel>(this.orderId);

            // Assert
            Assert.Equal(expected, order.Id);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateTheOrder()
        {
            // Arrange
            var expectedItem = new Order
            {
                Id = this.orderId,
                OrderedBy = new ApplicationUser
                {
                    FirstName = "firstName",
                    LastName = "lastName",
                    City = "Sofia",
                    Country = "Bulgaria",
                    PhoneNumber = "+359 898 31 26 13",
                    Adress = "Sofia St",
                    Email = "working@cases.net",
                },
                OrderStatus = OrderStatus.Completed,
            };
            var fakeOrders = new List<Order>
            {
                new Order
                {
                    Id = this.orderId,
                    OrderedById = this.userId,
                    OrderedBy = new ApplicationUser
                {
                    Id = this.userId,
                    FirstName = "firstName  ",
                    LastName = "lastName  ",
                    City = "Sofia  ",
                    Country = "Bulgaria  ",
                    PhoneNumber = "+359 898 31 26 13  ",
                    Adress = "Sofia St  ",
                    Email = "working@cases.net  ",
                },
                    OrderStatus = OrderStatus.Completed,
                },
                new Order
                {
                    Id = this.orderId + 1,
                    OrderedById = this.userId + "1",
                    OrderedBy = new ApplicationUser
                {
                    Id = this.userId + "2",
                    FirstName = "firstName",
                    LastName = "lastName",
                    City = "Sofia",
                    Country = "Bulgaria",
                    PhoneNumber = "+359 898 31 26 13",
                    Adress = "Sofia St",
                    Email = "working@cases.net",
                },
                    OrderStatus = OrderStatus.Completed,
                },
            };
            var fakeUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = this.userId,
                    OrderId = this.orderId,
                },
            };
            this.orderRepository.Setup(s => s.All())
                .Returns(fakeOrders.AsQueryable());
            this.orderRepository.Setup(s => s.Update(It.IsAny<Order>()))
                .Callback((Order item) =>
                {
                    var searchedItem = fakeOrders.FirstOrDefault(x => x.Id == item.Id);
                    searchedItem.OrderStatus = item.OrderStatus;
                });
            var usersRepository = new Mock<IRepository<ApplicationUser>>();
            usersRepository.Setup(s => s.Update(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser model) =>
                {
                    var searchedItem = fakeUsers.FirstOrDefault(x => x.Id == model.Id);

                    searchedItem.FirstName = model.FirstName;
                    searchedItem.LastName = model.LastName;
                    searchedItem.PhoneNumber = model.PhoneNumber;
                    searchedItem.Email = model.Email;
                    searchedItem.Adress = model.Adress;
                    searchedItem.City = model.City;
                    searchedItem.Country = model.Country;
                });
            var service = new OrderService(this.orderRepository.Object, null, usersRepository.Object);

            var model = new OrderEditInputModel
            {
                Id = this.orderId,
                FirstName = "firstName",
                LastName = "lastName",
                City = "Sofia",
                Country = "Bulgaria",
                PhoneNumber = "+359 898 31 26 13",
                Adress = "Sofia St",
                Email = "working@cases.net",
                OrderStatus = OrderStatus.Completed.ToString(),
            };

            // Act
            await service.UpdateAsync(model);

            // Assert
            var actualItem = fakeOrders.FirstOrDefault(x => x.Id == this.orderId);

            // Assert
            Assert.Equal(expectedItem.OrderedBy.FirstName, actualItem.OrderedBy.FirstName);
            Assert.Equal(expectedItem.OrderedBy.LastName, actualItem.OrderedBy.LastName);
            Assert.Equal(expectedItem.OrderedBy.PhoneNumber, actualItem.OrderedBy.PhoneNumber);
            Assert.Equal(expectedItem.OrderedBy.Email, actualItem.OrderedBy.Email);
            Assert.Equal(expectedItem.OrderedBy.Adress, actualItem.OrderedBy.Adress);
            Assert.Equal(expectedItem.OrderedBy.City, actualItem.OrderedBy.City);
            Assert.Equal(expectedItem.OrderedBy.Country, actualItem.OrderedBy.Country);
        }
    }
}
