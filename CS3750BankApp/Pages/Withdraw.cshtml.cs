using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Pages
{
    public class WithdrawModel : PageModel
    {
        public string negativeAccountMsg { get; set; }
        public int accountNum { get; set; }
        [BindProperty]
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
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }
            WithdrawDetails.WithdrawFrom = Int32.Parse(Request.Form["withdraw"]);

            double deposit = Convert.ToDouble(WithdrawDetails.TransferAmmount);
            deposit = deposit * 100;
            int amount = Convert.ToInt32(deposit);
            //int amount = DbRepository.ConvertToSmallAmount(Int32.Parse(WithdrawDetails.TransferAmmount));

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
            negativeAccountMsg = "Not enough money in account!";
            OnGet();
            return Page();
        }

    }
    public class WithdrawDetails
    {
        public int WithdrawFrom { get; set; }
        [Required, RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Amount must be in format 00.00 ")]
        [Display(Name = "Amount:")]
        public decimal TransferAmmount { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }

    }
}
