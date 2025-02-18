namespace Basket.API.Models;


public class ShoppingCart : EntityId<ObjectId>
{
    public Guid UserId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = [];

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);


    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }

    public ShoppingCart()
    {
      
    }
}
