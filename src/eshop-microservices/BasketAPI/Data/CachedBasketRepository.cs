
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BasketAPI.Data
{
    public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repo.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;

        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
               return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);

            var basket = await repo.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
              await repo.StoreBasket(cart, cancellationToken);

            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart),cancellationToken);

            return cart;
        }
    }
}
