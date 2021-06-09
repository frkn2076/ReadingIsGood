using Shared.Audit;
using System.ComponentModel.DataAnnotations;

namespace Product.DataAccess.Entities
{
    public class ProductEntity : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string Volume { get; set; }
    }
}
