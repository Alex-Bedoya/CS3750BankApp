namespace CS3750BankApp.Models
{
    public class User
    {
        public int AccountNumber { get; set; }
        public string Salt { get; set; }
        public string HashedPass { get; set; }


    }
}
