using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CS3750BankApp.Pages
{
    public class TransferModel : PageModel
    {
        public int accountNum { get; set; }
        public List<Account> Accounts { get; set; }
        [BindProperty]
        public TransferDetails TransferDetails { get; set; }


        public void OnGet()
        {
            accountNum = (int)HttpContext.Session.GetInt32("_Account");
            Accounts = DbRepository.GetSubAccounts(accountNum);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            TransferDetails.SelectedTo = Int32.Parse(Request.Form["subTo"]);
            TransferDetails.SelectedFrom = Int32.Parse(Request.Form["subFrom"]);
            int amount = DbRepository.ConvertToSmallAmount(Int32.Parse(TransferDetails.TransferAmmount));

            if (DbRepository.CheckFunds(TransferDetails.SelectedFrom, amount))
            {
                Transactions transaction = new Transactions();
                transaction.Amount = amount;
                accountNum = (int)HttpContext.Session.GetInt32("_Account");
                transaction.AccountNumber = accountNum;
                transaction.TransactionType = "Transfer";
                transaction.Description = TransferDetails.Description;

                //get account type
                transaction.Sender = DbRepository.GetAccountType(TransferDetails.SelectedFrom);
                transaction.Reciever = DbRepository.GetAccountType(TransferDetails.SelectedTo);

                transaction.Date = DateTime.Now.ToString();

                DbRepository.CreateTransaction(transaction);
                DbRepository.ManageFunds(TransferDetails.SelectedTo, amount);
                DbRepository.ManageFunds(TransferDetails.SelectedFrom, (amount * -1));

                return RedirectToPage("AccountsView");
            }
            return Page();
        }
    }

    public class TransferDetails
    {
        public int SelectedTo { get; set; }
        public int SelectedFrom { get; set; }
        [Required]
        [Display(Name = "Amount:")]
        public string TransferAmmount { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}
