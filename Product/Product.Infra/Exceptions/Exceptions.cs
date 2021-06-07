using Shared.Error;

namespace Product.Infra.Exceptions
{
    public class ProductNotFoundException : DefinedException
    {
        public ProductNotFoundException() : base("Product not found", 200) { }
    }
}
