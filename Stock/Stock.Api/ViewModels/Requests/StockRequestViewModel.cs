namespace Stock.Api.ViewModels.Requests
{
    public record StockRequestViewModel
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
