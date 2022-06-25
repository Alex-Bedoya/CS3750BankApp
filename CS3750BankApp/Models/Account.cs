using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public int AccountNumber { get; set; }
        [StringLength(10)]
        public string Type { get; set; }//checking, saving, loan
        public int Balance { get; set; }

        public Account(int accountNumber, string type, int balance)
        {
            AccountNumber = accountNumber;
            Type = type;
            Balance = balance;
        }
    }
}
