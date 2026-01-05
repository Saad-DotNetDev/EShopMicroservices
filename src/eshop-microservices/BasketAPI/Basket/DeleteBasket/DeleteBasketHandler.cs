
namespace BasketAPI.Basket.DeleteBasket
{

    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResults>;

    public record DeleteBasketResults(bool isSuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        }

    }
    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResults>
    {
        public async Task<DeleteBasketResults> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
            {

          await  basketRepository.DeleteBasket(request.UserName, cancellationToken);
                   return new DeleteBasketResults(true);
            }
    }
}
