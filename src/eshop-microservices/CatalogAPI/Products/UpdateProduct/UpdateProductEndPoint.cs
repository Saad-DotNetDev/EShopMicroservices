
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid id, string Name, List<string> Category, string Description, string ImageFile, decimal Amount);
    public record UpdateProductResponse(bool isSuccess);
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }
}
