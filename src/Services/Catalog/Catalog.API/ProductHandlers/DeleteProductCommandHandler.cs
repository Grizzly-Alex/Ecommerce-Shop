﻿namespace Catalog.API.ProductHandlers;


public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

internal class DeleteProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
