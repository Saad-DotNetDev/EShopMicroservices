 
namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Cateogry) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product>Products);
    internal class GetProductByCategoryQueryHandler
        (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) 
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
           var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Cateogry))
                .ToListAsync();

            return new GetProductByCategoryResult(products);
        }
    }
}
 