namespace Basket.API.Models;

public sealed class ShoppingCartItem
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductName { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid ProductId { get; set; }
}
