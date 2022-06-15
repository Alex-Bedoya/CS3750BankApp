using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS3750BankApp.Pages
{
    public class Index1Model : PageModel
    {
       private readonly BankDbContext bankDb;

        public User user { get; set; }

        public Index1Model(BankDbContext bankDb)
        {
            this.bankDb = bankDb;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
            {
                await bankDb.AddAsync(user);
                await bankDb.SaveChangesAsync();
                return RedirectToPage("AccountView");
            }
            else
            {
                return Page();
            }
        }
    }
}
