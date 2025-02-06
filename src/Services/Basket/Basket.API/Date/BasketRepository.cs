namespace Basket.API.Date;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(Guid id, CancellationToken token = default)
    {
        var basket =  await session.LoadAsync<ShoppingCart>(id, token);

        return basket is null 
            ? throw new BasketNotFoundException(id) 
            : basket;  
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(token);

        return cart;
    }

    public async Task<bool> DeleteBasket(Guid Id, CancellationToken token = default)
    {
        session.Delete<ShoppingCart>(Id);
        await session.SaveChangesAsync(token);

        return true;
    }
}
