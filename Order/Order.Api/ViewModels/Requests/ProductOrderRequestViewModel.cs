namespace Order.Api.ViewModels.Requests
{
    public record ProductOrderRequestViewModel
    {
        public int OrderId { get; init; }
        public int ProductId { get; init; }
    }
}
