﻿namespace Catalog.API.Products.CreateProduct;


public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-product",
            async (CreateProductRequest request, ISender sender, CancellationToken token) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command, token);
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/create-product/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}
