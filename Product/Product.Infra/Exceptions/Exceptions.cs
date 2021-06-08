using Shared.Error;

namespace Product.Infra.Exceptions
{
    public class ProductNotFoundException : DefinedException
    {
        public ProductNotFoundException() : base("Product not found", 200) { }
    }

    public class ProductAlreadyExistsException : DefinedException
    {
        public ProductAlreadyExistsException() : base("This product already exists.", 201) { }
    }
}
