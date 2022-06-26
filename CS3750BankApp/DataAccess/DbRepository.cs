
﻿using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;

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


        public static List<Transactions> GetAllTransactions(int accountNumber) {
            List<Transactions> transactions;
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    transactions = db.Transactions.Where(q => q.AccountNumber == accountNumber).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return transactions;
        }

        

        public static List<Transactions> GetTransactions(int accountNumber, string accountType) 
        {
            List<Transactions> transactions;
            try
            {
                using (BankDbContext db = new BankDbContext()) {
                    transactions = db.Transactions.Where(q => (q.AccountNumber == accountNumber) && ( q.Sender == accountType || q.Reciever == accountType) ).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return transactions;
        
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

                    //create the 3 accounts associated with a user
                    Account ac = new Account(user.AccountNumber, "Checking", 0);
                    CreateAccount(ac);
                    ac = new Account(user.AccountNumber, "Savings", 0);
                    CreateAccount(ac);
                    ac = new Account(user.AccountNumber, "Other", 0);
                    CreateAccount(ac);

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static User FindUser(string id)
        {
            User user;
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    user = db.Users.First(q => q.AccountNumber == Int32.Parse(id));
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        internal static Boolean UserExistsByEmail(string email)
        {

            User user = new User();
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    user = db.Users.First(q => q.Email == email);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }          
        }

        public static string HashPassword(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }

        public static bool CheckFunds(int SubAccount, int amount)
        {
            bool check = false;
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    Console.WriteLine(db.Accounts.First(q => q.ID == SubAccount).Balance);
                    check = db.Accounts.First(q => q.ID == SubAccount).Balance >= amount;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return check;
        }

        public static void CreateTransaction(Transactions transactions)
        {
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    db.Transactions.Add(transactions);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ManageFunds(int subAccount, int amount)
        {
            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    db.Accounts.First(q => q.ID == subAccount).Balance += amount;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetAccountType(int subAccountId)
        {
            string accountType = null;

            try
            {
                using (BankDbContext db = new BankDbContext())
                {
                    accountType = db.Accounts.First(q => q.ID == subAccountId).Type;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return accountType;
        }

        public static int ConvertToLargeAmount(int ammount)
        {
            return ammount / 100;
        }
        /// <summary>
        /// Numbers go Vroom
        /// </summary>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public static int ConvertToSmallAmount(int ammount)
        {
            return ammount * 100;
        }

    } 
   
}
