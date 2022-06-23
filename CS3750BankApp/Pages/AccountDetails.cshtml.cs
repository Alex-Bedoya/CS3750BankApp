using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;

namespace CS3750BankApp.Pages
{
    public class AccountDetailsModel : PageModel
    {
        public string accType;
        public int accNum;
        


        public async void OnGet(string Type, int AccNum)
        {
            accType = Type;
            accNum = AccNum;
        }
    }
}
