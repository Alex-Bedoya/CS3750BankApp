
using CS3750BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CS3750BankApp.DataAccess
{
    public class BankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

               optionsBuilder.UseSqlServer(
                 //@"Server=DESKTOP-KMK9963\SQLEXPRESS;Database=BankApp;Trusted_Connection=True;");
              //Brendan
              //Ryan Desktop
              @"Server=DESKTOP-D61Q4DT;Database=BankApp;Trusted_Connection=True;");
              //Ryan Laptop LAPTOP-E5G80MMC
              //@"Server=LAPTOP-E5G80MMC;Database=BankApp;Trusted_Connection=True;");


            //@"Server=DESKTOP-B6VT6G1\SQLEXPRESS;Database=BankApp;Trusted_Connection=True;"); // alex

            //optionsBuilder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;"); //Adam
        }
    }
}
