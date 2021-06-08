using Shared.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Order.DataAccess.Entities
{
    public class OrderEntity : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public List<int> Products { get; set; }

        public static implicit operator OrderEntity(int id) => new OrderEntity() { Id = id };
    }
}
