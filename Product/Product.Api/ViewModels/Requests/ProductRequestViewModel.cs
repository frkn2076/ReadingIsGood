namespace Product.Api.ViewModels.Requests
{
    public record ProductRequestViewModel
    {
        public string Name { get; init; }
        public string Color { get; init; }
        public string Weight { get; init; }
        public string Volume { get; init; }
    }
}
