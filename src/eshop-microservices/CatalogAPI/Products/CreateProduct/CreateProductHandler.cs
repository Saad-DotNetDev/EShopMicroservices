namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name , List<string> Category, string Description , string ImageFile, decimal Amount) 
        : ICommand<CreateProductResult> { }
 
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
        internal class CreateProductCommandHandler(IDocumentSession session)
                : ICommandHandler<CreateProductCommand, CreateProductResult>
        {
            public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
 
                var product = new Product
                {
                    Name = command.Name,
                    Category = command.Category,
                    Description = command.Description,
                    ImageFile = command.ImageFile,
                    Price = command.Amount
                };

                session.Store(product);
                await session.SaveChangesAsync(cancellationToken);
                return new CreateProductResult(product.Id);
            }
        }
    }
}
