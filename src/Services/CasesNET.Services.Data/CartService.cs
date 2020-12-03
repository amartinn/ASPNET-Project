namespace CasesNET.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Services.Mapping;

    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> cartItemRepository;
        private readonly IRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Case> caseRepository;

        public CartService(
            IDeletableEntityRepository<CartItem> cartItemRepository,
            IDeletableEntityRepository<Cart> cartRepository,
            IDeletableEntityRepository<Case> caseRepository)
        {
            this.cartItemRepository = cartItemRepository;
            this.cartRepository = cartRepository;
            this.caseRepository = caseRepository;
        }

        public async Task AddItemByIdAndUserIdAsync(string caseId, string userId)
        {
            var userCart = this.cartRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId);
            var @case = this.caseRepository.All()
                .FirstOrDefault(x => x.Id == caseId);
            if (userCart == null)
            {
                userCart = new Cart { UserId = userId };
                userCart.Items.Add(new CartItem
                {
                    Quantity = 1,
                    CaseId = caseId,
                    CartId = userCart.Id,
                });
                await this.cartRepository.AddAsync(userCart);
            }
            else
            {
                var caseInUserCart = userCart.Items.FirstOrDefault(x => x.CaseId == caseId);
                if (caseInUserCart != null)
                {
                    caseInUserCart.Quantity++;
                }
                else
                {
                    userCart.Items.Add(new CartItem
                    {
                        Quantity = 1,
                        CartId = userCart.Id,
                        CaseId = caseId,
                    });
                }

                this.cartRepository.Update(userCart);
            }

            await this.cartRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllItemsByUserId<T>(string userId)
        {
            var userCart = this.cartRepository
              .All()
              .Where(x => x.UserId == userId)
              .FirstOrDefault();

            return userCart == null ? new List<T>() : userCart.Items
              .AsQueryable()
              .To<T>()
              .ToList();
        }

        public int GetItemsCountByUserId(string userId)
        {
            var items = this.cartRepository
           .All()
           .Where(x => x.UserId == userId)
            .ToList()
            .FirstOrDefault()
            ?.Items
            .Select(x => new
            {
                x.Quantity,
            })
            .Sum(x => x.Quantity);

            return items ?? 0;
        }

        public async Task RemoveItemByIdAndUserIdAsync(string cartItemId, string userId)
        {
            var cartItem = this.cartItemRepository.All()
                .Where(x => x.Id == cartItemId && x.Cart.UserId == userId).FirstOrDefault();
            this.cartItemRepository.Delete(cartItem);
            await this.cartItemRepository.SaveChangesAsync();
        }
    }
}
