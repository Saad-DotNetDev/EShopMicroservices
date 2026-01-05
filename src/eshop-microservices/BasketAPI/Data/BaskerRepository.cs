
using BasketAPI.Exceptions;

namespace BasketAPI.Data
{
    public class BaskerRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
                await session.SaveChangesAsync(cancellationToken);
            return true;
        }
 
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var loadBasket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return loadBasket is null ? throw new BasketNotFoundException(userName) : loadBasket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart;
        }
    }
}
