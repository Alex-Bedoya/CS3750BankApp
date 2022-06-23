namespace CS3750BankApp.Models
{
    public class Transactions
    {
        public int ID { get; set; }

        public int AccountNumber { get; set; }

        public string Date { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string TransactionType { get; set; }


    }
}
