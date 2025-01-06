﻿using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(
    IDocumentSession session,
    ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get product by id: {query.Id}");

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null) 
            throw new ProductNotFoundException();

        return new GetProductByIdResult(product);
    }
}
