
namespace BasketAPI.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string  UserName);
    public record DeleteBasketResponse(bool isSuccess);
    public class DeleteBAsketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var results = await sender.Send(new DeleteBasketCommand(userName));

                var response = results.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteProduct")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
