﻿using MediatR;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : IRequest<CreateProductResult>;
public record CreateProductResult(Guid Id);


internal sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}