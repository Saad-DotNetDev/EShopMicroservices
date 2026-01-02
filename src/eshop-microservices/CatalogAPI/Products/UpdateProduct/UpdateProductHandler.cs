
using CatalogAPI.Exceptions;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, string Name, List<string> Category, string Description, string ImageFile, decimal price)
        :ICommand<UpdateProductResult>;
        public class UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidatior : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required").Length(2,100).WithMessage("Name must be greater than 2 and less than 100");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
           var product = await session.LoadAsync<Product>(command.id, cancellationToken);
            if ( product is null)
            {
                throw new ProductNotFoundException(command.id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
