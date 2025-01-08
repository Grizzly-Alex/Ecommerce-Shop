﻿namespace Catalog.API.CQRS;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get product by id: {query.Id}");

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken)
            ?? throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(product);
    }
}
