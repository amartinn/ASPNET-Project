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

        public CartServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(FakeCartItem).GetTypeInfo().Assembly);
        }
        [Fact]
        public async Task AddItemByIdAndUserIdAsyncMethodShouldAddItemToUser()
        {
            // Arrange
            var cartList = new List<Cart>();
            var caseList = new List<Case> { new Case { Id = this.caseId } };
            var user = new ApplicationUser { Id = this.userId };
            var cartRepo = new Mock<IDeletableEntityRepository<Cart>>();
            var caseRepo = new Mock<IDeletableEntityRepository<Case>>();
            var usermanager = new Mock<FakeUserManager>();

            cartRepo.Setup(s => s.All())
                .Returns(cartList.AsQueryable());
            cartRepo.Setup(s => s.AddAsync(It.IsAny<Cart>()))
                .Callback((Cart cart) => cartList.Add(cart));
            cartRepo.Setup(s => s.Update(It.IsAny<Cart>()))
                .Callback((Cart cart) =>
                {
                    cartList.Remove(cartList[0]);
                    cartList.Add(cart);
                });

            caseRepo.Setup(s => s.All())
                .Returns(caseList.AsQueryable());
            usermanager.Setup(s => s.FindByIdAsync(this.userId))
                .ReturnsAsync(user);

            // Act
            var cartService = new CartService(null, cartRepo.Object, caseRepo.Object, usermanager.Object);

            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);
            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);

            // Assert
            var totalItems = user.Cart.Items.Sum(x => x.Quantity);
            var expected = 2;
            Assert.Equal(expected, cartService.GetItemsCountByUserId(this.userId));
            Assert.Equal(expected, totalItems);
        }

        [Fact]
        public void GetAllItemsByUserIdMethodShouldReturnCorrectItems()
        {
            // Arrange
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
                    UserId = this.userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem(),
                        new CartItem(),
                        new CartItem(),
                    },
                },
            };

            var cartRepo = new Mock<IDeletableEntityRepository<Cart>>();

            cartRepo.Setup(s => s.All())
                .Returns(fakeCart.AsQueryable());
            // Act
            var cartService = new CartService(null, cartRepo.Object, null, null);

            var items = cartService.GetAllItemsByUserId<FakeCartItem>(this.userId);

            // Assert
            var expected = 3;
            Assert.Equal(expected, items.Count());
        }

        [Fact]
        public void GetItemsCountByUserIdMethodShouldReturnZeroIfNoItemsInCart()
        {
            // Arrange
            var fakeCart = new List<Cart>()
            {
                new Cart
                {
                    UserId = this.userId,
                },
            };
            var cartRepo = new Mock<IDeletableEntityRepository<Cart>>();

            cartRepo.Setup(s => s.All())
                .Returns(fakeCart.AsQueryable());

            // Act
            var service = new CartService(null, cartRepo.Object, null, null);

            var count = service.GetItemsCountByUserId(this.userId);

            // Assert
            var expected = 0;
            Assert.Equal(expected, count);
        }

        [Fact]
        public async Task RemoveItemByIdAndUserIdAsyncMethodShouldRemoveTheItem()
        {
            // Arrange
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
            var cartItemRepo = new Mock<IDeletableEntityRepository<CartItem>>();
            cartItemRepo.Setup(s => s.All())
               .Returns(fakeCart.AsQueryable());
            cartItemRepo.Setup(s => s.Delete(It.IsAny<CartItem>()))
                .Callback((CartItem item) =>
                {
                    fakeCart.Remove(item);
                });
            var cartService = new CartService(cartItemRepo.Object, null, null, null);
            await cartService.RemoveItemByIdAndUserIdAsync(this.cartItemId, this.userId);

            // Assert
            var expected = 2;
            Assert.Equal(expected, fakeCart.Count());
            Assert.DoesNotContain(fakeCart, x => x.Id == this.cartItemId);
        }
    }
}
