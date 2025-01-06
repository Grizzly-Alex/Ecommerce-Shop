namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/get-products", async (ISender sender, CancellationToken token) =>
        {
            var result = await sender.Send(new GetProductsQuery(), token);
            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
