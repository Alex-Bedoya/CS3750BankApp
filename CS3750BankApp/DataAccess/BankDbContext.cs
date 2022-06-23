
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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-B6VT6G1\SQLEXPRESS;Database=BankApp;Trusted_Connection=True;"); //Alex
            //optionsBuilder.UseSqlServer("server=localhost;database=BankApp;uid=root;password="); //Adam

        }
    }
}
