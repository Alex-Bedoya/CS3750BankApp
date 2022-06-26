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
        [BindProperty]
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
            if (!ModelState.IsValid) {
                OnGet();
                return Page(); }

           
            DepositDetails.Deposit = Int32.Parse(Request.Form["deposit"]);


            //double deposit = Double.Parse(Request.Form["deposit"]);
            //deposit = deposit * 100;
            //int amount = Convert.ToInt32(deposit);
            //int amount= 
            //int amount = DbRepository.ConvertToSmallAmount(Int32.Parse(DepositDetails.TransferAmmount));

            double deposit = Convert.ToDouble(DepositDetails.TransferAmmount);
            deposit = deposit * 100;
            int amount = Convert.ToInt32(deposit);


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
        [Required]
        public int Deposit { get; set; }
        [Required, RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage ="Amount must be in format 00.00 ")]
        [Display(Name = "Amount:")]
        public decimal TransferAmmount { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}

