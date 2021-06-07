namespace Product.Business.DTOs
{
    public record ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Color { get; init; }
        public string Weight { get; init; }
        public string Volume { get; init; }
    }
}
