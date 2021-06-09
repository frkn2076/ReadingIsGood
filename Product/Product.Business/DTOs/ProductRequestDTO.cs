namespace Product.Business.DTOs
{
    public record ProductRequestDTO
    {
        public string Name { get; init; }
        public string Weight { get; init; }
        public string Volume { get; init; }
    }
}
