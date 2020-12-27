namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> cartItemRepository;
        private readonly IRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Case> caseRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartService(
            IDeletableEntityRepository<CartItem> cartItemRepository,
            IDeletableEntityRepository<Cart> cartRepository,
            IDeletableEntityRepository<Case> caseRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.cartItemRepository = cartItemRepository;
            this.cartRepository = cartRepository;
            this.caseRepository = caseRepository;
            this.userManager = userManager;
        }

        public async Task AddItemByIdAndUserIdAsync(string caseId, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var @case = this.caseRepository.All()
                .FirstOrDefault(x => x.Id == caseId);
            if (user.Cart == null)
            {
                user.Cart = new Cart { UserId = userId };
                var item = new CartItem
                {
                    Quantity = 1,
                    CaseId = caseId,
                    CartId = user.Cart.Id,
                };
                user.Cart.Items.Add(item);
                @case.CartItemId = item.Id;
                await this.cartRepository.AddAsync(user.Cart);
                this.caseRepository.Update(@case);
            }
            else
            {
                var caseInUserCart = user.Cart.Items.FirstOrDefault(x =>    x.CaseId == caseId);
                if (caseInUserCart != null)
                {
                    caseInUserCart.Quantity++;
                }
                else
                {
                    user.Cart.Items.Add(new CartItem
                    {
                        Quantity = 1,
                        CartId = user.Id,
                        CaseId = caseId,
                    });
                }

                this.cartRepository.Update(user.Cart);
            }

            await this.cartRepository.SaveChangesAsync();
            await this.caseRepository.SaveChangesAsync();
            await this.userManager.UpdateAsync(user);
        }

        public IEnumerable<T> GetAllItemsByUserId<T>(string userId)
            => this.cartRepository
              .All()
              .Where(x => x.UserId == userId && x.IsDeleted == false)
              .FirstOrDefault()
              .Items
               .AsQueryable()
               .To<T>()
               .ToList();

        public int GetItemsCountByUserId(string userId)
            => this.cartRepository
           .All()
           .Where(x => x.UserId == userId)
            .ToList()
            .FirstOrDefault()?
            .Items
            .Select(x => new
            {
                x.Quantity,
            })
            .Sum(x => x.Quantity) ?? 0;

        public async Task RemoveItemByIdAndUserIdAsync(string cartItemId, string userId)
        {
            var cartItem = this.cartItemRepository.All()
                .FirstOrDefault(x => x.Id == cartItemId && x.Cart.UserId == userId);
            this.cartItemRepository.Delete(cartItem);
            await this.cartItemRepository.SaveChangesAsync();
        }

        public async Task RemoveCartByIdAndUserIdAsync(string cartId, string userId)
        {
            var cart = this.cartRepository.All()
                .FirstOrDefault(x => x.Id == cartId && x.UserId == userId);
            this.cartRepository.Delete(cart);
            await this.cartRepository.SaveChangesAsync();
        }
    }
}
