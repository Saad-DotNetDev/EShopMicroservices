namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart ShoppingCart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndPoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                if (request.ShoppingCart is null)
                    return Results.BadRequest("Cart cannot be null");

                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);
            })
                .WithName("CreateProduct")
                .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
