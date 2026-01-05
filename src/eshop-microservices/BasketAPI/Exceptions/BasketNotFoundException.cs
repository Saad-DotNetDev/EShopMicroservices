 

namespace BasketAPI.Exceptions
{
    public class BasketNotFoundException : NotFoundExceptions
    {
        public BasketNotFoundException(string userName) : base("Basket", userName )
        {
            
        }
    }
}
