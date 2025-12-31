
namespace CatalogAPI.Products.GetProducts
{
    public record getProductResponse(IEnumerable<Product>Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery());
                var response =   result.Adapt<getProductResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<getProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}
