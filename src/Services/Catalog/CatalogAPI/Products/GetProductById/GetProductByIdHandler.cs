

using System.Runtime.InteropServices;

namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductsByIdQueryHandler(IDocumentSession session,ILogger<GetProductsByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query,CancellationToken token)
    {
        logger.LogInformation("Query by id -> {Id}", query.Id);
        // var product = await session.Query<Product>()
        // .FirstOrDefaultAsync(p => p.Id == query.Id, token);
        var product = await session.LoadAsync<Product>(query.Id, token);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}