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
       private readonly BankDbContext bankDb;

        

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
            user.HashedPass = HashPassword(Encoding.UTF8.GetBytes(pass), Encoding.UTF8.GetBytes(salt));
            DbRepository.CreateUser(user);

            
        }
        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        public static string HashPassword(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }
    }
}
