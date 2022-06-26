using CS3750BankApp.DataAccess;
using CS3750BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace CS3750BankApp.Pages
{
    public class Index1Model : PageModel
    {
      

        /*public Index1Model(BankDbContext bankDb)
        {
            this.bankDb = bankDb;
        }*/

        public void OnGet()
        {
            Console.WriteLine(GenerateSalt());

        }

        public void OnPost()
        {
            User user = new User();
            string salt = GenerateSalt();
            user.Salt = salt;
            user.Email = Request.Form["email"];
            string pass = Request.Form["password"];
            user.HashedPass = DbRepository.HashPassword(Encoding.UTF8.GetBytes(pass), Encoding.UTF8.GetBytes(salt));
            DbRepository.CreateUser(user);
            //set the current account nubmer in the session
            HttpContext.Session.SetInt32("_Account", user.AccountNumber);
            Response.Redirect("/AccountsView");


        }
        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        
    }
}
