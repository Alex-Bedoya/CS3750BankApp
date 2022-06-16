using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;

namespace CS3750BankApp.Pages
{
    public class AccountDetailsModel : PageModel
    {
        private BankDbContext _dbContext;
        //private DbRepository repo;

        public AccountDetailsModel(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public AccountDetailsModel(DbRepository dbRepository)
        //{
        //    repo = dbRepository;
        //}

        public List<Transactions> transactions { get; set; }



        public async void OnGet()
        {
            transactions = _dbContext.Transactions.ToList();
            //transactions = repo.GetTransactions("Savings");
        }
    }
}
