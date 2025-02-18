namespace Basket.API.Date;

internal class BasketDbContext : IMongoDbContext<ShoppingCart>
{
    public IMongoCollection<ShoppingCart> collection { get; }

    public BasketDbContext(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        collection = client.GetDatabase(settings.Value.DatabaseName)
            .GetCollection<ShoppingCart>(settings.Value.BasketsCollectionName);
    }
}
