using Shared.Audit;
using System.ComponentModel.DataAnnotations;

namespace Stock.DataAccess.Entities
{
    public class StockEntity : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
