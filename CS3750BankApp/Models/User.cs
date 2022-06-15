using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Models
{
    public class User
    {
        [Key]
        public int AccountNumber { get; set; }
      
        public string? Salt { get; set; }
      
        public string? HashedPass { get; set; }

        public string? Email { get; set; }


    }
}
