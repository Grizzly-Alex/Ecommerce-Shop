namespace Basket.API.Date
{
    public interface IMongoDbContext<T> where T : EntityId<ObjectId>
    {
        public IMongoCollection<T> collection { get; }
    }
}
