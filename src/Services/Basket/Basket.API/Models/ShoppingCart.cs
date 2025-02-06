namespace Basket.API.Models;

public class ShoppingCart
{
    public Guid UserId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);


    public ShoppingCart(Guid userId)
    {
        UserId = userId;

    }

    public ShoppingCart()
    {
      
    }
}
