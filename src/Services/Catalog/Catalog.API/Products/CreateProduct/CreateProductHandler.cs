using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);


internal class CreateProductHandler(
    IDocumentSession session,
    ILogger<UpdateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Create new product");

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);   

        return new CreateProductResult(product.Id);
    }
}
