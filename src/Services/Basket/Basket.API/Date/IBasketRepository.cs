namespace Basket.API.Date;

public interface IBasketRepository
{
    public Task<ShoppingCart> GetBasket(Guid Id, CancellationToken token = default);
    public Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token = default);
    public Task<bool> DeleteBasket(Guid Id, CancellationToken token = default);
}
