using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS3750BankApp.Pages
{
    public class AccountsModel : PageModel
    {
        public List<Account> Accounts { get; set; }
        public void OnGet()
        {
            int accountNum = (int)HttpContext.Session.GetInt32("_Account");

            Accounts = DbRepository.GetSubAccounts(accountNum);
        }
    }
}
