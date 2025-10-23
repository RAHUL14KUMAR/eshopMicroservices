//using BuildingBlocks.CQRS;
//using CatalogAPI.Models;
//using Marten;
namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session):ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand cmd,CancellationToken token)
    {
        //business logic to create a product

        var product = new Product
        {
            Name = cmd.Name,
            Category = cmd.Category,
            Description = cmd.Description,
            ImageFile = cmd.ImageFile,
            Price = cmd.Price
        };

        //save to database
        session.Store(product);
        await session.SaveChangesAsync(token);

        return new CreateProductResult(product.Id);

        // throw new NotImplementedException();
    }
}
