﻿using Shared.Audit;
using System.ComponentModel.DataAnnotations;

namespace Account.DataAccess.Entities
{
    public class Registration : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
