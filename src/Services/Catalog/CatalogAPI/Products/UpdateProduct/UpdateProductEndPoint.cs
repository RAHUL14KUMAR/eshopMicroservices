using System.Security.Cryptography.X509Certificates;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",async(UpdateProductRequest request,ISender sender) =>
        {
            var cmd = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(cmd);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}