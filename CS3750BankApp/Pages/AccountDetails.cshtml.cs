using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;

namespace CS3750BankApp.Pages
{
    public class AccountDetailsModel : PageModel
    {
        public string accType;
        


        public async void OnGet(string Type)
        {
            accType = Type;
        }
    }
}
