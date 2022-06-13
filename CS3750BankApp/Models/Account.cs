namespace CS3750BankApp.Models
{
    public class Account
    {

        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }

        //DB will take care of this later.
        public Account(int iD, string accountNumber, string type, int balance)
        {
            ID = iD;
            AccountNumber = accountNumber;
            Type = type;
            Balance = balance;
        }
    }
}
