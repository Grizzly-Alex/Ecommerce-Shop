namespace Catalog.API.Endpoints;


public record DeleteProductResponse(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender, CancellationToken token) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id), token);
            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product"); ;
    }
}
