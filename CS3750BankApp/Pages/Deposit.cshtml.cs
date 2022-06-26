using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Pages
{
    public class DepositModel : PageModel
    {
        public int accountNum { get; set; }
        public List<Account> Accounts { get; set; }
        [BindProperty]
        public DepositDetails DepositDetails { get; set; }


        public void OnGet()
        {
            accountNum = (int)HttpContext.Session.GetInt32("_Account");
            Accounts = DbRepository.GetSubAccounts(accountNum);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            DepositDetails.Deposit = Int32.Parse(Request.Form["deposit"]);

            int amount = DbRepository.ConvertToSmallAmount(Int32.Parse(DepositDetails.TransferAmmount));

            if (amount > 0)
            {
                Transactions transaction = new Transactions();
                transaction.Amount = amount;
                accountNum = (int)HttpContext.Session.GetInt32("_Account");
                transaction.AccountNumber = accountNum;
                transaction.TransactionType = "Deposit";
                transaction.Description = DepositDetails.Description;

                //get account type

                transaction.Reciever = DbRepository.GetAccountType(DepositDetails.Deposit);

                transaction.Date = DateTime.Now.ToString();

                DbRepository.CreateTransaction(transaction);
                DbRepository.ManageFunds(DepositDetails.Deposit, amount);

                return RedirectToPage("AccountsView");

            }

            return Page();
        }
    }

    public class DepositDetails
    {
        public int Deposit { get; set; }
        [Required]
        [Display(Name = "Amount:")]
        public string TransferAmmount { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}

