using CS3750BankApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.Models;

namespace CS3750BankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        /*private readonly BankDbContext bankDb;

        public IndexModel(BankDbContext bankDb)
        {
            this.bankDb = bankDb;
        }*/

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

       // public IEnumerable<User> user { get; set; }

        public void OnGet()
        {

        }
        /*
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
        }*/
    }
}