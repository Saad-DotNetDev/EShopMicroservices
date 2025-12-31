
namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetPrdouctByCategoryResponse(IEnumerable<Product>Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
                async (string category, ISender sender) =>
             {
                 var result = await sender.Send(new GetProductByCategoryQuery(category));
                 var response = result.Adapt<GetPrdouctByCategoryResponse>();

                 return Results.Ok(response);

             })
                 .WithName("GetProductByCategory")
                 .Produces<GetPrdouctByCategoryResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Get Product By Category")
                 .WithDescription("Get Product By Category");
        }
    }
}
