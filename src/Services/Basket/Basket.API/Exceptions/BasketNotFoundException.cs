namespace Basket.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(Guid Id) : base("Basket", $"with UserId {Id}")
    {
    }
}
