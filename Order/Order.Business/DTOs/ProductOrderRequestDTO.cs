namespace Order.Business.DTOs
{
    public class ProductOrderRequestDTO
    {
        public int AccountId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
