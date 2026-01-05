 
namespace BasketAPI.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                 var reuslt = await sender.Send(new GetBasketQuery(userName));

                var response = reuslt.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
            
        }
    }
}
