using Shared.Error;

namespace Stock.Infra.Exceptions
{
    public class StockNotFoundException : DefinedException
    {
        public StockNotFoundException() : base("Stock not found", 300) { }
    }
}
