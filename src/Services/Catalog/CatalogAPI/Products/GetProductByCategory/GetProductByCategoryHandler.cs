namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Product);

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query,CancellationToken token)
    {
        // logger.LogInformation("Query by category -> {@query}", query);

        var products = await session.Query<Product>()
            .Where(products => products.Category.Contains(query.Category)).ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}