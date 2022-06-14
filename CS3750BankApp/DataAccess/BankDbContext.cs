
using CS3750BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CS3750BankApp.DataAccess
{
    public class BankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=BankApp;Trusted_Connection=True;");
        }
    }
}
