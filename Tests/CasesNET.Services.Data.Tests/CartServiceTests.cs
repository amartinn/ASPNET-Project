namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
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

        [Fact]
        public async Task AddItemByIdAndUserIdAsyncMethodShouldAddItemToUser()
        {
            // Arrange
            var cartList = new List<Cart>();
            var caseList = new List<Case> { new Case { Id = caseId } };

            var cartRepo = new Mock<IDeletableEntityRepository<Cart>>();
            var caseRepo = new Mock<IDeletableEntityRepository<Case>>();

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

            // Act
            var cartService = new CartService(null, cartRepo.Object, caseRepo.Object);

            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);
            await cartService.AddItemByIdAndUserIdAsync(this.caseId, this.userId);

            // Assert
            Assert.Equal(2, cartService.GetItemsCountByUserId(this.userId));
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

            AutoMapperConfig.RegisterMappings(typeof(FakeCartItem).GetTypeInfo().Assembly);

            // Act
            var cartService = new CartService(null, cartRepo.Object ,null);

            var items = cartService.GetAllItemsByUserId<FakeCartItem>(this.userId);

            // Assert
            Assert.Equal(3, items.Count());
        }

        [Fact]
        public async Task RemoveItemByIdAndUserIdAsyncMethodShouldRemoveTheItem()
        {
            // Arrange
            var fakeCart = new List<CartItem>()
            {
                        new CartItem{
                            CaseId = this.caseId,
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
            var cartService = new CartService(cartItemRepo.Object, null, null);

            await cartService.RemoveItemByIdAndUserIdAsync(this.caseId, this.userId);

            // Assert
            Assert.Equal(2, fakeCart.Count());
            Assert.DoesNotContain(fakeCart, x => x.CaseId == this.caseId);
        }
    }
}
