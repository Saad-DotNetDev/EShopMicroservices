using BuildingBlockks.Exceptions;

namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException : NotFoundExceptions
    {
        public ProductNotFoundException(Guid id) : base("Product ",id)
        {
            
        }
    }
}
