using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand cmd, CancellationToken token)
    {
        // logger.LogInformation("Query fro delete ->{@cmd}", cmd);

        session.Delete<Product>(cmd.Id);
        await session.SaveChangesAsync(token);

        return new DeleteProductResult(true);
    }
}