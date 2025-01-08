namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", $"with id {Id}")
    {
    }

    public ProductNotFoundException(string category) : base("Product", $"with category {category}")
    {
    }
}
