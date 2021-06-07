namespace Product.Business.DTOs
{
    public record ProductIntervalRequestDTO
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
