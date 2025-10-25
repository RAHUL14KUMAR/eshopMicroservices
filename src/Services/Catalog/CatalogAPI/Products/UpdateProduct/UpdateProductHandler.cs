using System.Windows.Input;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult>Handle(UpdateProductCommand cmd, CancellationToken token)
    {
        // logger.LogInformation("Query by category -> {@cmd}", cmd);
        var product = await session.LoadAsync<Product>(cmd.Id, token);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = cmd.Name;
        product.Category = cmd.Category;
        product.Description = cmd.Description;
        product.ImageFile = cmd.ImageFile;
        product.Price = cmd.Price;

        session.Update(product);
        await session.SaveChangesAsync(token);

        return new UpdateProductResult(true);
    }
}