using Shared.Audit;
using System.ComponentModel.DataAnnotations;

namespace Account.DataAccess.Entities
{
    public class Authentication : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public int RefreshTokenSeconds { get; set; }
    }
}
