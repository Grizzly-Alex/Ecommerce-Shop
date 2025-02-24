namespace Basket.API.Date
{
    internal interface IMongoDbContext<T> where T : class, new()
    {
        public IMongoCollection<T> collection { get; }
    }
}
