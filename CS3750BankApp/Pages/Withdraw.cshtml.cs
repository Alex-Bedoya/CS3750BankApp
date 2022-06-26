using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Pages
{
    public class WithdrawModel : PageModel
    {

        public int accountNum { get; set; }
        public List<Account> Accounts { get; set; }
        [BindProperty]
        public WithdrawDetails WithdrawDetails { get; set; }


        public void OnGet()
        {
            accountNum = (int)HttpContext.Session.GetInt32("_Account");
            Accounts = DbRepository.GetSubAccounts(accountNum);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            WithdrawDetails.WithdrawFrom = Int32.Parse(Request.Form["withdraw"]);
            int amount = DbRepository.ConvertToSmallAmount(Int32.Parse(WithdrawDetails.TransferAmmount));

            if (DbRepository.CheckFunds(WithdrawDetails.WithdrawFrom, amount))
            {
                Transactions transaction = new Transactions();
                transaction.Amount = amount;
                accountNum = (int)HttpContext.Session.GetInt32("_Account");
                transaction.AccountNumber = accountNum;
                transaction.TransactionType = "Transfer";
                transaction.Description = WithdrawDetails.Description;

                //get account type
                transaction.Sender = DbRepository.GetAccountType(WithdrawDetails.WithdrawFrom);
                

                transaction.Date = DateTime.Now.ToString();

                DbRepository.CreateTransaction(transaction);
                DbRepository.ManageFunds(WithdrawDetails.WithdrawFrom, (amount * -1));

                return RedirectToPage("AccountsView");
            }
            return Page();
        }

    }
    public class WithdrawDetails
    {
        public int WithdrawFrom { get; set; }
        [Required]
        [Display(Name = "Amount:")]
        public string TransferAmmount { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }

    }
}
