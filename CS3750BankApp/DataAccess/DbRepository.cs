using CS3750BankApp.Models;

namespace CS3750BankApp.DataAccess
{
    public class DbRepository
    {
       public static List<Account> GetSubAccounts(int accountNumber)
       {
            List<Account> accounts;
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    accounts = db.Accounts.Where(q => q.AccountNumber == accountNumber).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return accounts;
        }

        public static void CreateAccount(Account account)
        {
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    db.Accounts.Add(account);
                    db.SaveChanges(); // make sure to save if adding, editing, or deleting from the database
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CreateUser(User user)
        {
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    } 
   
}
