using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS3750BankApp.Models
{
    public class User
    {
        [Key]
        public int AccountNumber { get; set; }

        [Required]
        public string? Salt { get; set; }

        [Required]
        public string? HashedPass { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
