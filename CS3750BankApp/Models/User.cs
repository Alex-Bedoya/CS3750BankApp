using System.ComponentModel.DataAnnotations;

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

        public string? Email { get; set; }
    }
}
