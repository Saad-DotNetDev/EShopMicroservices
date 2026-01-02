
namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.id).NotEmpty().WithMessage("Id is Required");
        }
    }
    public class DeleteProductCommandHandler
        (IDocumentSession session,ILogger<DeleteProductCommandHandler>logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
