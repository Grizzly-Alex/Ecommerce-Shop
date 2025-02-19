namespace Basket.API.Date
{
    internal interface IMongoDbContext<T> where T : EntityId<ObjectId>
    {
        public IMongoCollection<T> collection { get; }
    }
}
