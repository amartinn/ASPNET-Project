namespace CasesNET.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Moq;
    using Xunit;

    public class CartServiceTests
    {
        private readonly string userId = "userId";
        private readonly string caseId = "caseId";
        private readonly string cartId = "cartId";
        private readonly string cartItemId = "cartItemid";
        private readonly Mock<IDeletableEntityRepository<Cart>> cartRepository;
        private readonly Mock<IDeletableEntityRepository<Case>> caseRepository;
        private readonly Mock<IDeletableEntityRepository<CartItem>> cartItemRepository;

        public CartServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCartItemModel).GetTypeInfo().Assembly);
            this.cartRepository = new Mock<IDeletableEntityRepository<Cart>>();
            this.caseRepository = new Mock<IDeletableEntityRepository<Case>>();
            this.cartItemRepository = new Mock<IDeletableEntityRepository<CartItem>>();
        }

        [Fact]
        public async Task AddItemByIdAndUserIdAsyncMethodShouldAddItemToUser()
        {
            // Arrange
            const int expected = 2;
            var cartList = new List<Cart>();
            var caseList = new List<Case> { new Case { Id = this.caseId } };
            var user = new ApplicationUser { Id = this.userId };
            var usermanager = new Mock<FakeUserManager>();

            this.cartRepository.Setup(s => s.All())
                .Returns(cartList.AsQueryable());
            this.cartRepository.Setup(s => s.AddAsync(It.IsAny<Cart>()))
                .Callback((Cart cart) => cartList.Add(cart));
            this.cartRepository.Setup(s => s.Update(It.IsAny<Cart>()))
                .Callback((Cart cart) =>
                {
                    cartList.Remove(cartList[0]);
                    cartList.Add(cart);
                });

            this.caseRepository.Setup(s => s.All())
                .Returns(caseList.AsQueryable());
            usermanager.Setup(s => s.FindByIdAsync(this.userId))
                .ReturnsAsync(user);

            // Act
            var cartService = new CartService(null, this.cartRepository.Object, this.caseRepository.Object, usermanager.Object);

            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);
            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);

            // Assert
            var totalItems = user.Cart.Items.Sum(x => x.Quantity);
            Assert.Equal(expected, cartService.GetItemsCountByUserId(this.userId));
            Assert.Equal(expected, totalItems);
        }

        [Fact]
        public void GetAllItemsByUserIdMethodShouldReturnCorrectItems()
        {
            // Arrange
            const int expected = 3;
            var fakeCart = new List<Cart>()
            {
                new Cart
                {
                    UserId = this.userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem(),
                        new CartItem(),
                        new CartItem(),
                    },
                },
                new Cart
                {
                    UserId = this.userId + "2",
                    Items = new List<CartItem>()
                    {
                        new CartItem(),
                        new CartItem(),
                        new CartItem(),
                    },
                },
            };

            this.cartRepository.Setup(s => s.All())
                .Returns(fakeCart.AsQueryable());
            var cartService = new CartService(null, this.cartRepository.Object, null, null);

            // Act
            var actual = cartService.GetAllItemsByUserId<FakeCartItemModel>(this.userId).Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetItemsCountByUserIdMethodShouldReturnZeroIfNoItemsInCart()
        {
            // Arrange
            const int expected = 0;
            var fakeCart = new List<Cart>()
            {
                new Cart
                {
                    UserId = this.userId,
                },
            };
            this.cartRepository.Setup(s => s.All())
                .Returns(fakeCart.AsQueryable());

            // Act
            var service = new CartService(null, this.cartRepository.Object, null, null);

            var actual = service.GetItemsCountByUserId(this.userId);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RemoveItemByIdAndUserIdAsyncMethodShouldRemoveTheItem()
        {
            // Arrange
            const int expected = 2;
            var fakeCart = new List<CartItem>()
            {
                        new CartItem
                        {
                            Id = this.cartItemId,
                            Cart = new Cart
                            {
                                 Id = this.cartId,
                                 UserId = this.userId,
                            },
                        },
                        new CartItem(),
                        new CartItem(),
            };

            // Act
            this.cartItemRepository.Setup(s => s.All())
               .Returns(fakeCart.AsQueryable());
            this.cartItemRepository.Setup(s => s.Delete(It.IsAny<CartItem>()))
                .Callback((CartItem item) =>
                {
                    fakeCart.Remove(item);
                });
            var cartService = new CartService(this.cartItemRepository.Object, null, null, null);
            await cartService.RemoveItemByIdAndUserIdAsync(this.cartItemId, this.userId);

            // Assert
            var actual = fakeCart.Count();
            Assert.Equal(expected, actual);
            Assert.DoesNotContain(fakeCart, x => x.Id == this.cartItemId);
        }

        [Fact]
        public async Task RemoveCartByIdAndUserIdAsyncMethodShouldDeleteTheCart()
        {
            // Arrange
            var fakeCarts = new List<Cart>
            {
            new Cart
            {
                Id = this.cartId,
                UserId = this.userId,
            },
            };
            this.cartRepository.Setup(s => s.All())
                .Returns(fakeCarts.AsQueryable());

            this.cartRepository.Setup(s => s.Delete(It.IsAny<Cart>()))
                .Callback((Cart cart) =>
                {
                    fakeCarts.Remove(cart);
                });

            var service = new CartService(null, this.cartRepository.Object, null, null);

            // Act
            await service.RemoveCartByIdAndUserIdAsync(this.cartId, this.userId);

            // Assert
            Assert.Empty(fakeCarts);
        }
    }
}
