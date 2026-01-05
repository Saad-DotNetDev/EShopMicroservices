 
namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart): ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator() 
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository ) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
             ShoppingCart cart  = command.ShoppingCart;

            await repository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
