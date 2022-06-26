using CS3750BankApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CS3750BankApp.Models;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CS3750BankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BankDbContext bankDb;

        public const string SessionKeyAccount = "_Account";

        [BindProperty]
        public Credential Credential { get; set; }

        /* public IndexModel(BankDbContext bankDb)
         {
             this.bankDb = bankDb;
         }*/



        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet() //can use to render data in the html section.
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //verify credentaials
            User user = VerifyCredentials(Credential.AccountNumber, Credential.Password);
            if (user != null)
            {
                // Creating security context
                /*  var claims = new List<Claim>
                  {
                      new Claim(ClaimTypes.Name, user.Email),
                      new Claim(ClaimTypes.UserData, Credential.AccountNumber)
                  };
                  var identidy = new ClaimsIdentity(claims, "CookieAuth");
                  ClaimsPrincipal principal = new ClaimsPrincipal(identidy);

                  await HttpContext.SignInAsync("CookieAuth", principal);*/
                /*Route route = new Route("/AccountsView/", "{account:int}", user.AccountNumber);*/
                HttpContext.Session.SetInt32(SessionKeyAccount, user.AccountNumber);
                //HttpContext.Session.SetString("Account", Credential.AccountNumber);

                return RedirectToPage("AccountsView");
            }

            return Page();
        }

        private User VerifyCredentials(string accountNumber, string password)
        {
            User user = new User();
            try
            {
                user = DbRepository.FindUser(accountNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                user = null;
            }

            if (user == null)
            {
                Console.WriteLine("no user");
            }
            else
            {
                try
                {
                    if (user.AccountNumber == Int32.Parse(accountNumber))
                    {
                        if (user.HashedPass == DbRepository.HashPassword(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(user.Salt)))
                        {
                            Console.WriteLine("logged in");
                        }
                        else { 
                            Console.WriteLine("wrong password");
                            user = null;
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e); 
                    user = null;
                }
            }
            return user;
        }
    }

    public class Credential
    {
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Correct { get; set; }
    }
}