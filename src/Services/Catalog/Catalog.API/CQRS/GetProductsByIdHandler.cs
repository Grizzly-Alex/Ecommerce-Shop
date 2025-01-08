namespace Catalog.API.CQRS;


public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(
    IDocumentSession session,
    ILogger<GetProductsByCategoryEndpoint> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get product by category: {query.Category}");

        var product = await session.Query<Product>()
            .Where(i => i.Category.Contains(query.Category))
            .ToListAsync(cancellationToken)
            ?? throw new ProductNotFoundException();

        return new GetProductsByCategoryResult(product);
    }
}
