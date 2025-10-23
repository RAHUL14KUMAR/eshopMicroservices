namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

// represents a sequence (collection) of items that you can iterate (loop) over. and that is why we use IEnumerable;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}