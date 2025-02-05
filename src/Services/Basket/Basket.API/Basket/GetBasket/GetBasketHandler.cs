namespace Basket.API.Basket.GetBasket;

public record GetbasketQuery(Guid UserId) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);


public class GetBasketHandler : IQueryHandler<GetbasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetbasketQuery query, CancellationToken cancellationToken)
    {
        var cart = new ShoppingCart(Guid.NewGuid());

        return new GetBasketResult(cart);
    }
}
